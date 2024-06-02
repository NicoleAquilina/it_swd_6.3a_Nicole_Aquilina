using WebApp.Models;

namespace WebApp.Services
{
    public interface IWatchlistService
    {
        public Task Create(List<string> videoId, string userId);
        public Task<WatchlistModel> Get(string userId);
        public Task Remove(string userId, string videoId);
    }
}
