namespace Campaign.Shared.DataBaseContext.Entities.OrderProductRemoved
{
    public class OrderProductRemoved
    {
        public int OrderId { get; private set; }
        public int SellerId { get; private set; }
        public int ProductId { get; private set; }
        public int ShipmentNumber { get; private set; }
        public int SequenceNumber { get; private set; }
        public int QtyProductHeld { get; private set; }
        public int QtyProductRemoved { get; private set; }
        public bool AllRemoved => QtyProductHeld <= 0 && QtyProductRemoved > 0;
    }
}
