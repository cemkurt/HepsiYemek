using System;

namespace HepsiYemek.Models
{
    public class ProductDetailResponseDto
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
    }

    public class ProductResponseDto
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryResponseDto CategoryId { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
    }
}
