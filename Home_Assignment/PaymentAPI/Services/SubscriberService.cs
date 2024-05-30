using Google.Cloud.PubSub.V1;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using MongoDB.Bson.IO;
using OrderAPI.Model;
using PaymentAPI.Controllers;
using PaymentAPI.Model;
using Newtonsoft.Json;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace PaymentAPI.Services
{
    public class SubscriberService :IHostedService, IDisposable
    {
        public Timer? _timer = null;
        public SubscriptionName subscriptionName;
        private readonly PaymentService PaymentService;
        public SubscriberService(IOptions<SubGCPSettings> settings, PaymentService Service)
        {
            subscriptionName = new SubscriptionName(settings.Value.Project, settings.Value.Sub);
            PaymentService = Service;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("SubscriberSerive is running");
            _timer = new Timer(async _ => await DoWork(_), null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private async Task DoWork(object? state)
        {
            SubscriberClient subscriber = await SubscriberClient.CreateAsync(subscriptionName);

            List<PubsubMessage> recievedMessages = new List<PubsubMessage>();

            await subscriber.StartAsync((msg, cancellationToken) =>
            {
                recievedMessages.Add(msg);
                Console.WriteLine($"Recieved Message {msg.MessageId} published at {msg.PublishTime.ToDateTime()}");

                var deserializedObject = JsonConvert.DeserializeObject<Order>(msg.Data.ToStringUtf8());
                Payment payment = new Payment()
                {
                    Price = deserializedObject.Price,
                    OrderId = deserializedObject.OrderId,
                    DatePaid = DateTime.Now,
                    UserId = deserializedObject.UserId
                };
                PaymentService.CreateAsync(payment);

                Console.WriteLine("Payment Accepted");

                subscriber.StopAsync(TimeSpan.FromSeconds(5));

                return Task.FromResult(SubscriberClient.Reply.Ack);
            });
        }

        public Task StopAsync(CancellationToken canellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

    }
}
