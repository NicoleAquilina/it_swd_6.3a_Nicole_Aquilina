using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using VideoCatalogueAPI.Model;

namespace WatchlistAPI.Model
{
    public class Watchlist
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? WatchListID { get; set; }
        public string? UserId { get; set; }
        public List<string> VideoIds { get; set; }
    }
}
