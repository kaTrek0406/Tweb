namespace JW.Domain
{
    public class Review
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public int JewelryItemId { get; set; }
        public JewelryItem JewelryItem { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}