using WebApp.Models;

namespace WebApp.Services
{
    public interface IOrderService
    {
        public Task Create(OrderModel order);
        public Task<IEnumerable<OrderModel>> GetAll(string userId);
        public Task<OrderModel> Get(string orderId);
    }
}
