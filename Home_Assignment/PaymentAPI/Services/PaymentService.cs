using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PaymentAPI.Model;

namespace PaymentAPI.Services
{
    public class PaymentService
    {
        private readonly IMongoCollection<Payment> _paymentCollection;

        public PaymentService(
            IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
                databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _paymentCollection = mongoDatabase.GetCollection<Payment>(
                databaseSettings.Value.PaymentCollectionName);
        }

        public async Task<Payment> GetAsync(string Id) =>
            await _paymentCollection.Find(x => x.OrderId == Id).FirstOrDefaultAsync();

        public async Task CreateAsync(Payment payment) =>
            await _paymentCollection.InsertOneAsync(payment);
    }
}
