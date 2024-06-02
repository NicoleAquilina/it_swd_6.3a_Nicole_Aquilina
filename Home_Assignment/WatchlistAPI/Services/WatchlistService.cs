using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WatchlistAPI.Model;

namespace WatchlistAPI.Services
{
    public class WatchlistService
    {
        private readonly IMongoCollection<Watchlist> _watchlistCollection;

        public WatchlistService(
            IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
                databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _watchlistCollection = mongoDatabase.GetCollection<Watchlist>(
                databaseSettings.Value.WatchlistCollectionName);
        }
        //retrieves all titles from watchlist
        public async Task<Watchlist> GetAllAsync(string userId) =>
            await _watchlistCollection.Find(x => x.UserId == userId).FirstOrDefaultAsync();
        //add a watchlist
        public async Task CreateAsync(Watchlist watchlist) =>
            await _watchlistCollection.InsertOneAsync(watchlist);

        // Remove a video ID from a user's watchlist
        public async Task RemoveAsync(string userId, string videoId)
        {
            var filter = Builders<Watchlist>.Filter.Eq(x => x.UserId, userId);
            var update = Builders<Watchlist>.Update.Pull(x => x.VideoIds, videoId);
            await _watchlistCollection.UpdateOneAsync(filter, update);
        }

        //Update watchlist
        public async Task UpdateAsync(string userId, List<string> videoIds)
        {
            var filter = Builders<Watchlist>.Filter.Eq(x => x.UserId, userId);
            var update = Builders<Watchlist>.Update.AddToSetEach(x => x.VideoIds, videoIds);
            await _watchlistCollection.UpdateOneAsync(filter, update);
        }
        //CheckVideoExists
        public async Task<bool> CheckVideoExistsInWatchlistAsync(string userId, List<string> videoIds)
        {
            var watchlist = await _watchlistCollection.Find(x => x.UserId == userId).FirstOrDefaultAsync();

            if (watchlist == null || watchlist.VideoIds == null)
            {
                return false;
            }

            // Check if any of the videoIds exist in the watchlist
            foreach (var videoId in videoIds)
            {
                if (watchlist.VideoIds.Contains(videoId))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
