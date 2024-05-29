using WebApp.Models;

namespace WebApp.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<VideoModel>> GetVideos(string genre);
        Task<List<string>> GetGenre();
        Task<VideoModel> GetVideo(string id);
    }
}
