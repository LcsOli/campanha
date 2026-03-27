namespace Campaign.Pooling.Commands.Orders.Get
{
    public record GetOrdersDetailCommand(int[] productsIds, DateTime initIn, DateTime endIn);
}
