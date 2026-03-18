using System.Net;

namespace Campaign.API.Configuration.Exceptions
{
    public class CompaignExceptions : Exception
    {
        public HttpStatusCode Code { get; private set; }
        public List<ExceptionMessage> Messages { get; private set; }
        public CompaignExceptions(HttpStatusCode code, List<ExceptionMessage> messages) 
        {
            Code = code;
            Messages = messages;
        }
    }
}
