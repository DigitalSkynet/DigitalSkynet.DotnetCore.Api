using System;
using System.Runtime.Serialization;
using DigitalSkynet.DotnetCore.Api.Enums;

namespace DigitalSkynet.DotnetCore.Api.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string message) : this(ResponseCode.UnexpectedError, message)
        { }

        public ApiException(ResponseCode statusCode, string message) : base(message)
        {
            StatusCode = (int) statusCode;
        }

        public ApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }

        public int StatusCode
        {
            get;
            set;
        }
    }
}
