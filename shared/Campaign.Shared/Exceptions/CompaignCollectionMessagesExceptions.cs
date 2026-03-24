using System.Net;

namespace Campaign.Shared.Exceptions
{
    public class CompaignCollectionMessagesExceptions : CompaignException
    {
        public List<ExceptionMessage> Messages { get; }
        public CompaignCollectionMessagesExceptions(HttpStatusCode code, List<ExceptionMessage> messages) : base(code)
        {
            Messages = messages;
        }
    }
}
