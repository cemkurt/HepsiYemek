using MongoDB.Bson;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HepsiYemek.Models
{
    public class Product : BaseModel
    {
        public  CategoryResponseDto CategoryId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
        
    }
}
