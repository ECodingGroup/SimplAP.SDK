using ECoding.SimpleApi.Core.SDK.Dto;
using Newtonsoft.Json;

namespace ECoding.SimpleApi.Core.SDK.Services
{
    public class SimpleApiAuthService
    {
        private readonly static object accessTokenLock = new Object();
        private static Tuple<string, DateTime> accessTokenStore = null;
        private readonly string _userName;
        private readonly string _password;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _tenant;
        private const string _scope = "SimpleAPI";
        private const string _tokenUrl = "https://api.simplap.com/connect/token";

        public SimpleApiAuthService(string userName, string password, string clientId, string clientSecret, string tenant)
        {
            _userName = userName;
            _password = password;
            _clientId = clientId;
            _clientSecret = clientSecret;
            _tenant = tenant;
        }

        internal async Task<string> GetAccessToken()
        {
            AccessToken responseObj = null;

            if (accessTokenStore == null || accessTokenStore.Item2 < DateTime.Now)
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
                        response.EnsureSuccessStatusCode();
                        responseObj = JsonConvert.DeserializeObject<AccessToken>(await response.Content.ReadAsStringAsync());
                    }
                }
                lock(accessTokenLock)
                {
                    accessTokenStore = new Tuple<string, DateTime>(responseObj.access_token, DateTime.Now.AddSeconds(responseObj.expires_in));
                }

                return responseObj.access_token;
            }
            else
            {
                return accessTokenStore.Item1;
            }
        }
    }
}
