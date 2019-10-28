using System.Collections.Generic;

namespace DigitalSkynet.DotnetCore.Api.Models
{
    public class PagedResponseModel<TPayload, TSummary> : ResponseModel<List<TPayload>>
        where TPayload : class, new()
    where TSummary : PayloadSummary
    {
        public PagedResponseModel(List<TPayload> items, TSummary summary) : this(items, summary, new List<Error>())
        { }

        public PagedResponseModel(List<TPayload> items, TSummary summary, List<Error> errors) : base(items, errors)
        {
            Payload = items;
            Summary = summary;
        }

        public PagedResponseModel()
        { }

        public TSummary Summary
        {
            get;
            set;
        }
    }
}
