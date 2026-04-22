namespace Campaign.API.Commands.SellerScore.Get
{
    public record GetSellersScoresByTeamCommand(int TeamId, string Filter, int Page, int Size );
}
