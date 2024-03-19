using System.Collections.Generic;

namespace JW.Domain
{
    public class JewelryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int MaterialId { get; set; }
        public Material Material { get; set; }
        public List<Review> Reviews { get; set; }
    }
}