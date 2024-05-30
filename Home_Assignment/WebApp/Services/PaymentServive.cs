using System.Net.Http.Json;
using WebApp.Models;

namespace WebApp.Services
{
    public class PaymentServive :IPaymentService
    {
        private readonly HttpClient _httpClient;
        public PaymentServive(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task Create(PaymentModel payment)
        {
            try
            {
                await _httpClient.PostAsJsonAsync("gateway/Payment/create", payment);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PaymentModel> Get(string orderId)
        {
            try
            {
                var payment = await _httpClient.GetFromJsonAsync<PaymentModel>($"/gateway/Payment/getOrder?orderId={orderId}");
                return payment;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
