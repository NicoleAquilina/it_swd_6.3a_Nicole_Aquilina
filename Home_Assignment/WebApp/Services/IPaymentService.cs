using WebApp.Models;

namespace WebApp.Services
{
    public interface IPaymentService
    {
        public Task Create(PaymentModel payment);
        public Task<PaymentModel> Get(string orderId);
    }
}
