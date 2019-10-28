namespace DigitalSkynet.DotnetCore.Api.Models
{
    public class PayloadSummary
    {
        public PayloadSummary(int pageNumber, int pageSize, int totalItems)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItems = totalItems;
        }

        public int PageNumber
        {
            get;
        }
        public int PageSize
        {
            get;
        }
        public int TotalItems
        {
            get;
        }
    }
}
