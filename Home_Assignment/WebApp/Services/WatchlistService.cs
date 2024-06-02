using System.Net.Http.Json;
using WebApp.Models;

namespace WebApp.Services
{
    public class WatchlistService : IWatchlistService
    {
        private readonly HttpClient _httpClient;
        public WatchlistService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task Create(List<string> videoId,string userId)
        {
            WatchlistModel watchlist = new WatchlistModel();
            watchlist.VideoIds = videoId;
            watchlist.UserId = userId;

            try
            {
                await _httpClient.PostAsJsonAsync("gateway/Watchlist/create", watchlist);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<WatchlistModel> Get(string userId)
        {
            try
            {
                var watchlist =await _httpClient.GetFromJsonAsync<WatchlistModel>($"gateway/Watchlist/get?userId={userId}");
                return watchlist;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Remove(string userId, string videoId)
        {
            try
            {
                await _httpClient.DeleteAsync($"gateway/Watchlist/delete?userId={userId}&videoId={videoId}");
              
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
