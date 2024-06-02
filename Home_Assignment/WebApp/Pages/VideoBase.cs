using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages
{
	public class VideoBase :ComponentBase
	{
		[Inject]
		public ICatalogService CatalogService { get; set; }
        [Inject]
        public IWatchlistService watchlistService { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public List<String> Genre { get; set; }

		public string SelectedGenre { get; set; }
        public IEnumerable<VideoModel> Videos { get; set; }

        public bool added { get; set; }

        protected override async Task OnInitializedAsync()
		{
			Genre = await CatalogService.GetGenre();
		}
        public async Task OnGenreChange(ChangeEventArgs e)
        {
            SelectedGenre = e.Value.ToString();
            Videos = await CatalogService.GetVideos(SelectedGenre);
        }

        public async Task AddWatchlist(string videoId)
        {
            List<string> video = new List<string>();
            video.Add(videoId);
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var userId = user.FindFirst("userId")?.Value;
            await watchlistService.Create(video,userId);
            NavigationManager.NavigateTo("/watchlist");
        }
    }
}
