namespace Campaign.Pooling.Commands.Orders.Get
{
    public record GetOrdersDetailCommand(int promotionCode, DateTime initIn, DateTime endIn);
}
