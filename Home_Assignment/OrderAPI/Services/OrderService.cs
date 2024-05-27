using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OrderAPI.Model;

namespace OrderAPI.Services
{
    public class OrderService
    {
        private readonly IMongoCollection<Order> _orderCollection;

        public OrderService(
            IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
                databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _orderCollection = mongoDatabase.GetCollection<Order>(
                databaseSettings.Value.OrderCollectionName);
        }

        public async Task<IEnumerable<Order>> GetAllAsync(string userId) =>
            await _orderCollection.Find(x => x.UserId == userId).ToListAsync();

        public async Task<Order> GetByOrderIdAsync(string Id) =>
            await _orderCollection.Find(x => x.OrderId == Id).FirstOrDefaultAsync();

        public async Task CreateAsync(Order order) =>
            await _orderCollection.InsertOneAsync(order);

    }
}
