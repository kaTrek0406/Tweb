namespace JW.Domain
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int JewelryItemId { get; set; }
        public JewelryItem JewelryItem { get; set; }
        public int Quantity { get; set; }
    }
}