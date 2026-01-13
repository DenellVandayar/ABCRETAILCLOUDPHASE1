using Azure.Storage.Queues;
using CloudPart1.Models;

namespace CloudPart1.Services
{
    public class QueueService
    {
        private readonly QueueClient _queueClient;
        private readonly QueueClient _OrderQueueClient;


        public QueueService(string connectionString)
        {
            _queueClient = new QueueClient(connectionString, "inventory");
            _OrderQueueClient = new QueueClient(connectionString, "order");

        }

        public async Task SendMessageToQueueAsync(string message)
        {
            await _queueClient.SendMessageAsync(message);
        }

        public async Task SendOrderAsync(string message)
        {
            await _OrderQueueClient.SendMessageAsync(message);
        }




    }
}
