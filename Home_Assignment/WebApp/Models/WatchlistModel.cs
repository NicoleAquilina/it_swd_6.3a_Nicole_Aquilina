namespace WebApp.Models
{
    public class WatchlistModel
    {
        public string? WatchListID { get; set; }
        public string? UserId { get; set; }
        public List<string> VideoIds { get; set; }
    }
}
