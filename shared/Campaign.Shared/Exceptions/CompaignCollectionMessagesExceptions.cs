using System.Net;

namespace Campaign.Shared.Exceptions
{
    public class CompaignCollectionMessagesExceptions : Exception
    {
        public HttpStatusCode Code { get; }
        public List<ExceptionMessage> Messages { get; }
        public CompaignCollectionMessagesExceptions(HttpStatusCode code, List<ExceptionMessage> messages)
        {
            Messages = messages;
            Code = code;
        }
    }
}
