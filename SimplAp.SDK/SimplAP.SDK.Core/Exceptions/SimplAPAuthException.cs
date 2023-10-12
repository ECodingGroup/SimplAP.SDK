using System;

namespace SimplAP.SDK.Core.Exceptions
{
    public class SimplAPAuthException: Exception
    {
        public SimplAPAuthException(string message):base(message) { }
        public SimplAPAuthException(string message, Exception innerException) : base(message, innerException) { }
    }
}
