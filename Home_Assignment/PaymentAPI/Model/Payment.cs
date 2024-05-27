using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PaymentAPI.Model
{
    public class Payment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? OrderId { get; set; }
        public decimal? Price { get; set; }
        public DateTime? DatePaid { get; set; }
    }
}
