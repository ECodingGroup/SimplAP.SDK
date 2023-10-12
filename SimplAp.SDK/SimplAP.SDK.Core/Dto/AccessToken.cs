using System;

namespace SimplAP.SDK.Core.Dto
{
    public class SimplAPAccessToken
    {
        public string AccessToken { get; set; }
        public long ExpiresIn { get; set; }
        public DateTimeOffset CreationTime { get; set; } = DateTimeOffset.Now;
        public string TokenType { get; set; }
        public string Scope { get; set; }
    }
}
