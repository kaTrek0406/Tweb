using System.Collections.Generic;

namespace JW.Domain
{
    public class Material
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<JewelryItem> JewelryItems { get; set; }
    }
}