namespace DigitalSkynet.DotnetCore.Api.Models
{
    public class Error
    {
        public string UserMessage
        {
            get;
            set;
        }

        public string InternalMessage
        {
            get;
            set;
        }

        public int Code
        {
            get;
            set;
        }

        public string MoreInfo
        {
            get;
            set;
        }
    }
}
