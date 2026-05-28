using Campaign.API.Commands.SellerScore.Create;

namespace Campaign.API.Handlers.SellerScore.RegisterSellerScore
{
    public interface IRegisterSellerScoreHandler
    {
        Task Handler(RegisterSellerScoreCommand cmd);
    }
}
