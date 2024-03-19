using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SimplAP.SDK.Core.Dto;
using SimplAP.SDK.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SimplAP.SDK.Core.Services
{
    public class SimplAPAuthService
    {
        private readonly string _userName;
        private readonly string _password;
        private readonly string _clientId = "SimpleAPI_App";
        private readonly string _clientSecret;
        private readonly string _tenant;
        private const string _scope = "SimpleAPI";
        private readonly string _tokenUrl = "https://api.simplap.com/connect/token";
        private readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };

        public SimplAPAuthService(string userName, string password, string clientSecret, string tenant)
        {
            if(string.IsNullOrEmpty(userName))
            { throw new ArgumentNullException(nameof(userName)); }
            if (string.IsNullOrEmpty(password))
            { throw new ArgumentNullException(nameof(password)); }
            if (string.IsNullOrEmpty(clientSecret))
            { throw new ArgumentNullException(nameof(clientSecret)); }
            if (string.IsNullOrEmpty(tenant))
            { throw new ArgumentNullException(nameof(tenant)); }

            _userName = userName;
            _password = password;
            _clientSecret = clientSecret;
            _tenant = tenant;
        }

        public async Task<SimplAPAccessToken> GetAccessTokenAsync()
        {
            using (var client = new HttpClient())
            {
                var formParams = new Dictionary<string, string>
                {
                    { "grant_type", "password" },
                    { "username", _userName},
                    { "password", _password },
                    { "client_id", _clientId },
                    { "scope", _scope },
                    { "client_secret", _clientSecret },
                    { "__tenant", _tenant }
                };

                using (var content = new FormUrlEncodedContent(formParams.AsEnumerable()))
                {
                    HttpResponseMessage response = await client.PostAsync(_tokenUrl, content);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    try
                    { response.EnsureSuccessStatusCode(); }
                    catch
                    { throw new SimplAPAuthException($"Unable to Generate Token, please check your settings. Status: {response.StatusCode}, Response: {responseContent}"); }

                    if (string.IsNullOrEmpty(responseContent)) throw new SimplAPAuthException("Unable to read token response. The API Could be down. Please contact support at support@simplap.com");

                    try
                    { return JsonConvert.DeserializeObject<SimplAPAccessToken>(responseContent, SerializerSettings); }
                    catch (Exception ex)
                    { throw new SimplAPAuthException($"Unable to deserialize token response. Token Response: {responseContent}, Error: {ex.Message}", ex); }
                }
            }
            
        }
    }
}
