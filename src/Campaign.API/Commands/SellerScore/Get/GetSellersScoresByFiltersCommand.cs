namespace Campaign.API.Commands.SellerScore.Get
{
    public record GetSellersScoresByFiltersCommand(int? TeamId, string Filter, int? Page, int? Size );
}
