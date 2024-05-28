using Google.Cloud.PubSub.V1;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OrderAPI.Model;

namespace OrderAPI.Services
{
    public class PublisherService
    {
        private readonly TopicName topicName;

        public PublisherService(IOptions<GCPSettings> settings) 
        {
            topicName = new TopicName(settings.Value.Project, settings.Value.Topic);
        }

        public async Task PublishMessage(Order order)
        {
            PublisherClient publisher = PublisherClient.Create(topicName);

            string o = JsonConvert.SerializeObject(order);
            string orderId = await publisher.PublishAsync(o);
            Console.WriteLine(orderId);

            await publisher.ShutdownAsync(TimeSpan.FromSeconds(15));
        }
    }
}
