
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OrderAPI.Model
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? OrderId { get; set; }
        public string? VideoId { get; set; }
        public string? UserId { get; set; }
        public decimal? Price { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}
