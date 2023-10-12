using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http;
using System.Linq;
using System;
using SimplAP.SDK.Core.Dto;
using SimplAP.SDK.Core.Dto.Shared;
using System.IO;
using SimplAP.SDK.Core.Exceptions;
using System.Net;

namespace SimplAP.SDK.Core.Services
{
    public class SimplAPService
    {
        private const string SimpleAPIEndpoint = "https://api.simplap.com/api/app/a-i";

        public async Task<MultiFileProcessingOutput> ProcessImageFile(MultiFileProcessingInput input, SimplAPAccessToken accessToken)
        {
            if (input == null)
            { throw new ArgumentNullException(nameof(input)); }
            if (input.ImageData == null)
            { throw new ArgumentNullException(nameof(input.ImageData)); }
            if (input.ProcessesToRun == null || !input.ProcessesToRun.Any()) 
            { throw new ArgumentException("You must select what processes you want to run.", nameof(input.ProcessesToRun)); }
            if(input.ModelType == default)
            { throw new ArgumentException("You must select what AI model you want to use.", nameof(input.ModelType)); }
            if (string.IsNullOrEmpty(accessToken?.AccessToken)) 
            { throw new ArgumentNullException(nameof(accessToken)); }
            if(DateTimeOffset.Now >= accessToken.CreationTime.AddSeconds(accessToken.ExpiresIn)) 
            { throw new ArgumentException($"The access token is expired, please issue a new token and repeat the call."); }

            using var client = new HttpClient();
                
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.AccessToken);

            using var memoryStream = new MemoryStream(input.ImageData);
            using var contentStream = new StreamContent(memoryStream);
            using var formData = new MultipartFormDataContent();
            
            StringContent processesToRun = new StringContent(string.Join(",", input.ProcessesToRun.ToArray()).ToLower());
            StringContent imageType = new StringContent(input.ImageType.ToString().ToLower());

            formData.Add(processesToRun, "ProcessesToRun");
            formData.Add(imageType, "ImageType");
            contentStream.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            formData.Add(contentStream, "File");

            var url = string.Format($"{SimpleAPIEndpoint}/{{0}}/process-image-file-multipart", input.ModelType.ToString().ToLower());
            HttpResponseMessage response = client.PostAsync(url, formData).Result;

            string responseString = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            { throw new SimplAPAuthException("The API call resulted in a Unauthorized, please check the validity of your access token"); }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            { throw new SimplAPAuthException("Forbidden, looks like your token doesn't have access to this service. You can set the permissions in the https://api.simplap.com user management screens."); }
            else if (response.StatusCode == HttpStatusCode.InternalServerError)
            { throw new SimplAPProcessingException($"Internal Server Error, please contact us at support@simplap.com, Response: {responseString}"); }
            else if (response.StatusCode != HttpStatusCode.OK)
            { throw new SimplAPProcessingException($"Unexpected issue, Status code: {response.StatusCode}, please contact us at support@simplap.com, Response: {responseString}"); }

            if (string.IsNullOrEmpty(responseString))
            { throw new SimplAPProcessingException("Unexpected issue, the response string is empty"); }

            try
            {
                return JsonConvert.DeserializeObject<MultiFileProcessingOutput>(responseString);
            }
            catch
            { throw new SimplAPProcessingException($"There has been an error deserializing the response. Please contact us at support@simplap.com. Response: {responseString}"); }
        }
    }
}
