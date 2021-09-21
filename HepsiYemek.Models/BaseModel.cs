using MongoDB.Bson;
using System;

namespace HepsiYemek.Models
{
    public class BaseModel : IBaseModel
    {
        public ObjectId Id { get; set; }
    }

    public interface IBaseModel
    {
        ObjectId Id { get; set; }
    }
}
