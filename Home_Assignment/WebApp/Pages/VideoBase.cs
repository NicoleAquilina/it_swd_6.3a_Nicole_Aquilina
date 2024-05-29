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
        public NavigationManager NavigationManager { get; set; }

        public List<String> Genre { get; set; }

		public string SelectedGenre { get; set; }
        public IEnumerable<VideoModel> Videos { get; set; }

        protected override async Task OnInitializedAsync()
		{
			Genre = await CatalogService.GetGenre();
		}
        public async Task OnGenreChange(ChangeEventArgs e)
        {
            SelectedGenre = e.Value.ToString();
            Videos = await CatalogService.GetVideos(SelectedGenre);
        }
    }
}
