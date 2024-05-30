using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages
{
    public class PaymentBase :ComponentBase
    {

        [Inject]
        public IPaymentService PaymentService { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public PaymentModel Payment { get; set; }
        [Parameter]
        public string OrderID { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Payment = await PaymentService.Get(OrderID);
        }
    }
}
