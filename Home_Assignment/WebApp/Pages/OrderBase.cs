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
                    showThankYouModal = true;
                }
                catch(Exception ex)
                {
                    // Handle any exceptions
                    Console.WriteLine($"Error placing order: {ex.Message}");
                }
            }
        }

    }
}
