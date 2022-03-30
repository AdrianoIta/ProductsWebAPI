using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ProductsWebAPI.Models
{
    public class ProductsModel
    {
        public ObjectId Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public decimal UnitPrice { get; set; }
    }
}