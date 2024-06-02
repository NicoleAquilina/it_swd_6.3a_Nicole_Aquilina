using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages
{
    public class OrderBase : ComponentBase
    {
        [Inject]
        public ICatalogService CatalogService { get; set; }
        [Inject]
        public IPaymentService PaymentService { get; set; }
        [Inject]
        public IOrderService OrderService { get; set; }
        [Inject]
        public IWatchlistService watchlistService { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string SelectedVideo { get; set; }

        public decimal Price { get; set; }

        public VideoModel Video { get; set; }
        public PaymentModel Payment { get; set; }

        public bool showThankYouModal { get; set; }

        protected override async Task OnInitializedAsync()
        {
            showThankYouModal = false;
            Video = await CatalogService.GetVideo(SelectedVideo);
        }

        protected async Task PlaceOrder()
        {
            if(Video != null)
            {
                try
                {
                    var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                    var user = authState.User;
                    var userId = user.FindFirst("userId")?.Value;

                    var order = new OrderModel();
                    order.VideoId = SelectedVideo;
                    order.UserId = userId;
                    order.Price = Price;
                    order.OrderDate = DateTime.UtcNow;

                    await OrderService.Create(order);
                    bool check = await CheckInWatchList(userId, SelectedVideo);
                    if(check == true)
                    {
                        //video is in the watchlist => Remove
                        await watchlistService.Remove(userId, SelectedVideo);
                    }
                    showThankYouModal = true;
                }
                catch(Exception ex)
                {
                    // Handle any exceptions
                    Console.WriteLine($"Error placing order: {ex.Message}");
                }
            }
        }


        protected async Task<bool> CheckInWatchList(string userId,string videoId)
        {
            WatchlistModel w = await watchlistService.Get(userId);

            if (w == null || w.VideoIds == null)
            {
                return false;
            }

            // Check if any of the videoIds exist in the watchlist
            foreach (var v in w.VideoIds)
            {
                if (w.VideoIds.Contains(v))
                {
                    return true;
                }
            }

            return false;
        }

    }
}
