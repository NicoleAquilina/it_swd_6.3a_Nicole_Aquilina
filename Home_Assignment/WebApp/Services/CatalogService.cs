using System.Net.Http.Json;
using WebApp.Models;

namespace WebApp.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;
        public CatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<string>> GetGenre()
        {
            List<string> genres = new List<string>();
            try
            {
                genres = await _httpClient.GetFromJsonAsync<List<string>>("/gateway/Video/genres");
                return genres;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<VideoModel> GetVideo(string id)
        {
            try
            {
                var video = await _httpClient.GetFromJsonAsync<VideoModel>($"/gateway/Video/title?Id={id}");
                return video;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<VideoModel>> GetVideos(string genre)
        {
            try
            {
                var videos = await _httpClient.GetFromJsonAsync<IEnumerable<VideoModel>>($"/gateway/Video/getbygenre?genre={genre}");
                return videos;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
