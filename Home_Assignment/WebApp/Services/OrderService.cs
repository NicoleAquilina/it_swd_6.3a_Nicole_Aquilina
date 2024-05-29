using System.Net.Http.Json;
using WebApp.Models;

namespace WebApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task Create(OrderModel order)
        {
            try
            {
                await _httpClient.PostAsJsonAsync("gateway/Order/create", order);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OrderModel> Get(string orderId)
        {
            try
            {
                var order = await _httpClient.GetFromJsonAsync<OrderModel>($"/gateway/Order/orderDetails?orderId={orderId}");
                return order;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<OrderModel>> GetAll(string userId)
        {

            try
            {
                var orders = await _httpClient.GetFromJsonAsync<IEnumerable<OrderModel>>($"/gateway/Order/orders?userId={userId}");
                return orders;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
