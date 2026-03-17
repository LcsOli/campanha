using System.Net;

namespace Campaign.API.Configuration.Exceptions
{
    public class CompaignExceptionResponse : Exception
    {
        public HttpStatusCode Code { get; private set; }
        public CompaignExceptionResponse(HttpStatusCode code, string message) : base(message)
        {
            Code = code;
        }
    }
}
