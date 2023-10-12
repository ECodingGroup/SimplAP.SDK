using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Text;
using ECoding.SimpleApi.Core.SDK.Dto;
using System.Net.Http.Headers;
using ECoding.SimpleApi.Core.SDK.Enums;

namespace ECoding.SimpleApi.Core.SDK.Services
{
    public class SimpleApiAIService
    {
        private const string SimpleAPIEndpoint = "https://api.simplap.com/api/app/a-i";
        private readonly SimpleApiAuthService _authService;
        public SimpleApiAIService(SimpleApiAuthService authService)
        {
            _authService = authService;
        }

        public async Task<MultiFileProcessingOutput> ProcessImageFile(AIModelType type, MultiFileProcessingInput input)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                    var jsonString = JsonConvert.SerializeObject(new MultiFileProcessingInputExtended(input, type), settings);
                    var accessToken = await _authService.GetAccessToken();
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                    using (var content = new StringContent(jsonString, Encoding.UTF8, "application/json"))
                    {
                        var url = string.Format($"{SimpleAPIEndpoint}/{{0}}/process-image-file", type.ToString().ToLower());
                        HttpResponseMessage response = client.PostAsync(url, content).Result;
                        response.EnsureSuccessStatusCode();

                        string responseString = await response.Content.ReadAsStringAsync();
                        if (string.IsNullOrEmpty(responseString))
                        {
                            return new MultiFileProcessingOutput();
                        }
                        var output = JsonConvert.DeserializeObject<MultiFileProcessingOutput>(responseString);
                        return output;
                    }
                }
                catch
                {
                    return new MultiFileProcessingOutput();
                }
            }
        }

        public async Task<MultiFileProcessingOutput> ProcessImageFileMultipart(AIModelType type, MultiFileProcessingMultipartInput input)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var accessToken = await _authService.GetAccessToken();
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

                    using (var fileStream = new StreamContent(input.FileStream))
                    using (var formData = new MultipartFormDataContent())
                    {
                        StringContent processesToRun = new StringContent(string.Join(",", input.ProcessesToRun.ToArray()).ToLower());
                        StringContent imageType = new StringContent(input.ImageType.ToString().ToLower());

                        formData.Add(processesToRun, "ProcessesToRun");
                        formData.Add(imageType, "ImageType");
                        fileStream.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                        formData.Add(fileStream, "File");

                        var url = string.Format($"{SimpleAPIEndpoint}/{{0}}/process-image-file-multipart", type.ToString().ToLower());
                        HttpResponseMessage response = client.PostAsync(url, formData).Result;
                        response.EnsureSuccessStatusCode();

                        string responseString = await response.Content.ReadAsStringAsync();
                        if (string.IsNullOrEmpty(responseString))
                        {
                            return new MultiFileProcessingOutput();
                        }
                        var output = JsonConvert.DeserializeObject<MultiFileProcessingOutput>(responseString);
                        return output;
                    }
                }
                catch
                {
                    return new MultiFileProcessingOutput();
                }
            }
        }

        public async Task<IEnumerable<AvailableProcessingDto>> GetAvailableImageProcessing()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var accessToken = await _authService.GetAccessToken();
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                    var url = $"{SimpleAPIEndpoint}/available-image-processing";
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    response.EnsureSuccessStatusCode();

                    string responseString = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(responseString))
                    {
                        return Enumerable.Empty<AvailableProcessingDto>();
                    }
                    var output = JsonConvert.DeserializeObject<IEnumerable<AvailableProcessingDto>>(responseString);
                    return output;
                }
                catch
                {
                    return Enumerable.Empty<AvailableProcessingDto>();
                }
            }
        }

        public async Task<GetNumberOfCallsForPeriodOutput> GetNumberOfCallsForPeriod(DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var accessToken = await _authService.GetAccessToken();
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                    var url = $"{SimpleAPIEndpoint}/number-of-calls-for-period";
                    var queryString = $"dateFrom={dateFrom}&dateTo={dateTo}";
                    HttpResponseMessage response = client.GetAsync($"{url}?{queryString}").Result;
                    response.EnsureSuccessStatusCode();

                    string responseString = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(responseString))
                    {
                        return new GetNumberOfCallsForPeriodOutput();
                    }
                    var output = JsonConvert.DeserializeObject<GetNumberOfCallsForPeriodOutput>(responseString);
                    return output;
                }
                catch
                {
                    return new GetNumberOfCallsForPeriodOutput();
                }
            }
        }

        public async Task<IEnumerable<NumberOfCallsPerDayDto>> GetNumberOfCallsPerDayForPeriod(DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var accessToken = await _authService.GetAccessToken();
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                    var url = $"{SimpleAPIEndpoint}/number-of-calls-per-day-for-period";
                    var queryString = $"dateFrom={dateFrom}&dateTo={dateTo}";
                    HttpResponseMessage response = client.GetAsync($"{url}?{queryString}").Result;
                    response.EnsureSuccessStatusCode();

                    string responseString = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(responseString))
                    {
                        return Enumerable.Empty<NumberOfCallsPerDayDto>();
                    }
                    var output = JsonConvert.DeserializeObject<IEnumerable<NumberOfCallsPerDayDto>>(responseString);
                    return output;
                }
                catch
                {
                    return Enumerable.Empty<NumberOfCallsPerDayDto>();
                }
            }
        }

        [Obsolete]
        public async Task<FileProcessingOutput> ProcessImage(FileProcessingInput input)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                    var jsonString = JsonConvert.SerializeObject(input, settings);
                    var accessToken = await _authService.GetAccessToken();
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                    using (var content = new StringContent(jsonString, Encoding.UTF8, "application/json"))
                    {
                        var url = string.Format($"{SimpleAPIEndpoint}/{{0}}/process-file", input.AIModel.ToString().ToLower());
                        HttpResponseMessage response = client.PostAsync(url, content).Result;
                        response.EnsureSuccessStatusCode();

                        string responseString = await response.Content.ReadAsStringAsync();
                        if (string.IsNullOrEmpty(responseString))
                        {
                            return new FileProcessingOutput();
                        }
                        var output = JsonConvert.DeserializeObject<FileProcessingOutput>(responseString);
                        return output;
                    }
                }
                catch
                {
                    return new FileProcessingOutput();
                }
            }
        }
    }
}
