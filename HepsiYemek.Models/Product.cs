using System;

namespace HepsiYemek.Models
{
    public class Product : BaseModel
    {
        public string CategoryId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
        
    }
}
