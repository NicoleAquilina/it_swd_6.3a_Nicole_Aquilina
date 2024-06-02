using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages
{
    public class WatchlistBase : ComponentBase
    {
        [Inject]
        public IWatchlistService watchlistService { get; set; }
        [Inject]
        public ICatalogService catalogService { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public WatchlistModel Watchlist { get; set; }

        public List<VideoModel> Video { get; set; } = new List<VideoModel>();

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var userId = user.FindFirst("userId")?.Value;
            Watchlist = await watchlistService.Get(userId);

            if (Watchlist?.VideoIds != null)
            {
                foreach (var videoId in Watchlist.VideoIds)
                {
                    var video = await catalogService.GetVideo(videoId);
                    if (video != null)
                    {
                        Video.Add(video);
                    }
                }
            }
        }

        protected async Task DeleteVideo(string videoId)
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var userId = user.FindFirst("userId")?.Value;
            await watchlistService.Remove(userId, videoId);
            // Refresh the page
            NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
        }
    }
}
