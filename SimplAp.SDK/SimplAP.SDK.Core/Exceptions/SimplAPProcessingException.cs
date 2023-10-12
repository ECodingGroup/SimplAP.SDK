using System;

namespace SimplAP.SDK.Core.Exceptions
{
    public class SimplAPProcessingException : Exception
    {
        public SimplAPProcessingException(string message):base(message) { }
        public SimplAPProcessingException(string message, Exception innerException) : base(message, innerException) { }
    }
}
