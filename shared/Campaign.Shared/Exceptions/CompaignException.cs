using System.Net;

namespace Campaign.Shared.Exceptions
{
    public class CompaignException : Exception
    {
        public HttpStatusCode Code { get; }
        public CompaignException(HttpStatusCode code, string message) : base(message)
        {
            Code = code;
        }

        protected CompaignException(HttpStatusCode code)
        {
            Code = code;
        }
    }
}
