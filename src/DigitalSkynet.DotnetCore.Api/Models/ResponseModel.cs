using System.Collections.Generic;

namespace DigitalSkynet.DotnetCore.Api.Models
{
    public class ResponseModel<T>
    {
        public ResponseModel(T payload) : this(payload, new List<Error>())
        { }

        public ResponseModel(T payload, List<Error> errors)
        {
            Payload = payload;
            Errors = errors;
        }

        public ResponseModel()
        { }

        public T Payload
        {
            get;
            set;
        }

        public List<Error> Errors
        {
            get;
            set;
        }
    }
}
