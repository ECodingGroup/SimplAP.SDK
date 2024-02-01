using System;

namespace SimplAP.SDK.Core.Dto
{
    public class SimplAPAccessToken
    {
        /// <summary>
        /// The token to use for Simpl AP
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// The expiration of the token in seconds
        /// </summary>
        public long ExpiresIn { get; set; }
        public DateTimeOffset CreationTime { get; set; } = DateTimeOffset.Now;
        public string TokenType { get; set; }
        public string Scope { get; set; }
    }
}
