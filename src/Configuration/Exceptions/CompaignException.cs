using System.Net;

namespace Campaign.API.Configuration.Exceptions
{
    public class CompaignException : Exception
    {
        public HttpStatusCode Code { get; private set; }
        public CompaignException(HttpStatusCode code, string message) : base(message)
        {
            Code = code;
        }
    }
}
