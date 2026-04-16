using Campaign.API.Commands.User.Update;

namespace Campaign.API.Handlers.User.InsertScoreByAccess
{
    public interface IInsertScoreByAccessHandler
    {
        Task Handle(InsertScoreByAccessCommand cmd);
    }
}
