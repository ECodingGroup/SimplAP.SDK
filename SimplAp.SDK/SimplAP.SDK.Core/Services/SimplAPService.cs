using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http;
using System.Linq;
using System;
using SimplAP.SDK.Core.Dto;
using System.IO;
using SimplAP.SDK.Core.Exceptions;
using System.Net;
using SimplAP.SDK.Core.ContractResolvers;

namespace SimplAP.SDK.Core.Services
{
    public class SimplAPService
    {
        private readonly string SimpleAPIBaseAddress = "api.simplap.com";
        private readonly string SimpleAPIAIEndpoint = "https://{0}/api/app/a-i";
        private readonly string SimpleAPIStatsEndpoint = "https://{0}/api/app/stats";

        public SimplAPService()
        {
            SimpleAPIAIEndpoint = string.Format(SimpleAPIAIEndpoint, SimpleAPIBaseAddress);
            SimpleAPIStatsEndpoint = string.Format(SimpleAPIStatsEndpoint, SimpleAPIBaseAddress);
        }

        private void ValidateAccessToken(SimplAPAccessToken accessToken)
        {
            if (string.IsNullOrEmpty(accessToken?.AccessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }

            if (DateTimeOffset.Now >= accessToken.CreationTime.AddSeconds(accessToken.ExpiresIn))
            {
                throw new SimplAPAuthException("The access token is expired, please issue a new token and repeat the call.");
            }
        }

        // Helper to read response content with #if to handle .NET 4.8 specifically
        private async Task<string> ReadContentAsStringAsync(HttpResponseMessage response)
        {
#if NET48
            var contentTask = response.Content.ReadAsStringAsync();
            Task.WaitAll(contentTask);
            return contentTask.Result;
#else
            return await response.Content.ReadAsStringAsync();
#endif
        }

        private async Task<T> ValidateStatusCodeAndGetResponse<T>(HttpResponseMessage response)
            where T : class
        {
            if (response == null)
            {
                throw new SimplAPProcessingException("Internal Server Error, please contact us at support@simplap.com, response is null");
            }

            string responseString = await ReadContentAsStringAsync(response);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new SimplAPAuthException("The API call resulted in Unauthorized; please check the validity of your access token.");
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new SimplAPAuthException("Forbidden; your token doesn’t have access to this service. You can manage that in user management.");
            }
            else if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new SimplAPProcessingException($"Internal Server Error, please contact support@simplap.com. Response: {responseString}");
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new SimplAPProcessingException($"Bad request, please verify your parameters. Response: {responseString}");
            }
            else if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new SimplAPProcessingException($"Unexpected issue, Status: {response.StatusCode}, Response: {responseString}");
            }

            if (string.IsNullOrEmpty(responseString))
            {
                throw new SimplAPProcessingException("Unexpected issue, the response string is empty.");
            }

            try
            {
                return JsonConvert.DeserializeObject<T>(responseString);
            }
            catch (Exception ex)
            {
                throw new SimplAPProcessingException(
                    $"Error deserializing the response. Response: {responseString}",
                    ex
                );
            }
        }

        /// <summary>
        /// Performs various AI processing tasks on the input image. 
        /// Please specify what AI processing tasks you want to perform.
        /// </summary>
        public async Task<ProcessingOutput> ProcessAIAsync(ProcessingExtendedInput input, SimplAPAccessToken accessToken)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (input.ImageData == null) throw new ArgumentNullException(nameof(input.ImageData));
            if (input.ProcessesToRun == null || !input.ProcessesToRun.Any())
                throw new ArgumentException("You must select what processes you want to run.", nameof(input.ProcessesToRun));
            if (input.ModelType == default)
                throw new ArgumentException("You must select which AI model you want to use.", nameof(input.ModelType));

            ValidateAccessToken(accessToken);

#if NET48
            // --- .NET Framework 4.8 code path (traditional using statements) ---
            using (var formData = new MultipartFormDataContent())
            using (var client = new HttpClient())
            {
                StringContent processesToRun = new StringContent(string.Join(",", input.ProcessesToRun.ToArray()).ToLower());
                formData.Add(processesToRun, "processesToRun");

                StringContent imageType = new StringContent(input.ImageType.ToString().ToLower());
                formData.Add(imageType, "imageType");

                StringContent disableObjectSegmentation = new StringContent(input.DisableObjectSegmentation.ToString().ToLower());
                formData.Add(disableObjectSegmentation, "disableObjectSegmentation");

                StringContent returnPdfFileBytesOnOutput = new StringContent(input.ReturnPdfFileBytesOnOutput.ToString().ToLower());
                formData.Add(returnPdfFileBytesOnOutput, "returnPdfFileBytesOnOutput");

                if (input.GenericScannerFieldsToUse != null && input.GenericScannerFieldsToUse.Any())
                {
                    StringContent genericScannerFieldsToUse = new StringContent(string.Join(",", input.GenericScannerFieldsToUse.ToArray()).ToLower());
                    formData.Add(genericScannerFieldsToUse, "genericScannerFieldsToUse");
                }

                if (input.ProcessesToRun.Contains(Enums.ImageAIProcessingType.CheckboxDetection) &&
                    input.CheckboxesToDetect != null &&
                    input.CheckboxesToDetect.Any())
                {
                    if (!input.CheckboxesToDetect.All(x => !string.IsNullOrEmpty(x.Id)))
                    {
                        var badCheckbox = input.CheckboxesToDetect.First(x => !string.IsNullOrEmpty(x.Id));
                        throw new ArgumentException($"Missing checkbox Id. pattern: {badCheckbox.CheckboxMatchingRegex}");
                    }

                    if (!input.CheckboxesToDetect.All(x => !string.IsNullOrEmpty(x.CheckboxMatchingRegex)))
                    {
                        var badCheckbox = input.CheckboxesToDetect.First(x => !string.IsNullOrEmpty(x.CheckboxMatchingRegex));
                        throw new ArgumentException($"Missing regex pattern for checkbox detection. CheckboxId: {badCheckbox.Id}");
                    }

                    var jsonSettings = new JsonSerializerSettings
                    {
                        ContractResolver = new LowercaseContractResolver()
                    };
                    StringContent checkboxes = new StringContent(JsonConvert.SerializeObject(input.CheckboxesToDetect, jsonSettings));
                    formData.Add(checkboxes, "checkboxesToDetect");
                }

                using (var imageStreamContent = new StreamContent(new MemoryStream(input.ImageData)))
                {
                    imageStreamContent.Headers.Add("Content-Type", "application/octet-stream");
                    var fileExtension = (input.ImageType == Enums.ProcessedImageType.Image) ? "jpg" : "pdf";
                    formData.Add(imageStreamContent, "file", $"file.{fileExtension}");

                    var url = $"{string.Format(SimpleAPIAIEndpoint, SimpleAPIBaseAddress)}/{input.ModelType.ToString().ToLower()}/process-multipart";
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.AccessToken);

                    var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = formData };
                    Task<HttpResponseMessage> sendTask = client.SendAsync(request);
                    Task.WaitAll(sendTask);
                    HttpResponseMessage response = sendTask.Result;

                    return await ValidateStatusCodeAndGetResponse<ProcessingOutput>(response);
                }
            }
#else
            // --- .NET 7, 8, 9 code path (modern using declarations) ---
            using var formData = new MultipartFormDataContent();
            using var client = new HttpClient();

            StringContent processesToRunStr = new StringContent(string.Join(",", input.ProcessesToRun.ToArray()).ToLower());
            formData.Add(processesToRunStr, "processesToRun");

            StringContent imageType = new StringContent(input.ImageType.ToString().ToLower());
            formData.Add(imageType, "imageType");

            StringContent disableObjectSegmentation = new StringContent(input.DisableObjectSegmentation.ToString().ToLower());
            formData.Add(disableObjectSegmentation, "disableObjectSegmentation");

            StringContent returnPdfFileBytesOnOutput = new StringContent(input.ReturnPdfFileBytesOnOutput.ToString().ToLower());
            formData.Add(returnPdfFileBytesOnOutput, "returnPdfFileBytesOnOutput");

            if (input.GenericScannerFieldsToUse != null && input.GenericScannerFieldsToUse.Any())
            {
                StringContent genericScannerFieldsToUse = new StringContent(string.Join(",", input.GenericScannerFieldsToUse.ToArray()).ToLower());
                formData.Add(genericScannerFieldsToUse, "genericScannerFieldsToUse");
            }

            if (input.ProcessesToRun.Contains(Enums.ImageAIProcessingType.CheckboxDetection) &&
                input.CheckboxesToDetect != null &&
                input.CheckboxesToDetect.Any())
            {
                if (!input.CheckboxesToDetect.All(x => !string.IsNullOrEmpty(x.Id)))
                {
                    var badCheckbox = input.CheckboxesToDetect.First(x => !string.IsNullOrEmpty(x.Id));
                    throw new ArgumentException($"Missing checkbox Id. pattern: {badCheckbox.CheckboxMatchingRegex}");
                }

                if (!input.CheckboxesToDetect.All(x => !string.IsNullOrEmpty(x.CheckboxMatchingRegex)))
                {
                    var badCheckbox = input.CheckboxesToDetect.First(x => !string.IsNullOrEmpty(x.CheckboxMatchingRegex));
                    throw new ArgumentException($"Missing regex pattern for checkbox detection. CheckboxId: {badCheckbox.Id}");
                }

                var jsonSettings = new JsonSerializerSettings
                {
                    ContractResolver = new LowercaseContractResolver()
                };
                StringContent checkboxes = new StringContent(JsonConvert.SerializeObject(input.CheckboxesToDetect, jsonSettings));
                formData.Add(checkboxes, "checkboxesToDetect");
            }

            using var imageStreamContent = new StreamContent(new MemoryStream(input.ImageData));
            imageStreamContent.Headers.Add("Content-Type", "application/octet-stream");
            var extension = (input.ImageType == Enums.ProcessedImageType.Image) ? "jpg" : "pdf";
            formData.Add(imageStreamContent, "file", $"file.{extension}");

            var endpointUrl = $"{string.Format(SimpleAPIAIEndpoint, SimpleAPIBaseAddress)}/{input.ModelType.ToString().ToLower()}/process-multipart";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.AccessToken);

            var responseNet = await client.PostAsync(endpointUrl, formData);
            return await ValidateStatusCodeAndGetResponse<ProcessingOutput>(responseNet);
#endif
        }

        /// <summary>
        /// Gets the list of available generic scanner fields to use for AI processing.
        /// </summary>
        public async Task<GetAvailableGenericScannerFieldsOutput> GetAvailableGenericScannerFieldsAsync(SimplAPAccessToken accessToken)
        {
            ValidateAccessToken(accessToken);

            var url = $"{string.Format(SimpleAPIAIEndpoint, SimpleAPIBaseAddress)}/available-generic-scanner-fields";

#if NET48
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.AccessToken);

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                Task<HttpResponseMessage> sendTask = client.SendAsync(request);
                Task.WaitAll(sendTask);
                HttpResponseMessage response = sendTask.Result;

                return await ValidateStatusCodeAndGetResponse<GetAvailableGenericScannerFieldsOutput>(response);
            }
#else
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.AccessToken);

            var responseNet = await client.GetAsync(url);
            return await ValidateStatusCodeAndGetResponse<GetAvailableGenericScannerFieldsOutput>(responseNet);
#endif
        }
    }
}
