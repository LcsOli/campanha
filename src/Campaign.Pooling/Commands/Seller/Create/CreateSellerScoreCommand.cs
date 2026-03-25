namespace Campaign.Pooling.Commands.Seller.Create
{
    public record CreateSellerScoreCommand(List<SellersScoreToCreateCommand> SellersToCreate);
}
