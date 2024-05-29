using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages
{
    public class OrderDetailsBase :ComponentBase
    {
        [Inject]
        public ICatalogService CatalogService { get; set; }
        [Inject]
        public IOrderService OrderService { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        public IEnumerable<OrderModel> Orders { get; set; }

        public VideoModel Video { get; set; }

        public string SelectedVideo { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var userId = user.FindFirst("userId")?.Value;
            Orders = await OrderService.GetAll(userId);
        }

        protected  async Task GetTitleOrder(string videoId)
        {
            Video = await CatalogService.GetVideo(SelectedVideo);
        }

    }
}
