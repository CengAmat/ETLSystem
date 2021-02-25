using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ETLSystem.Service;

namespace ETLSystem.Listener
{
    public interface IListener
    {
        Task ExecuteAsync(CancellationToken stoppingToken);
    }

    public class Listener
    {
        private readonly IMessageHandler messageHandler;
        private readonly string queueUrl;

        public Listener(IMessageHandler messageHandler)
        {
            this.messageHandler = messageHandler;
        }

        public async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine($"From Execute : {DateTime.UtcNow}");
            while (!stoppingToken.IsCancellationRequested)
            {
                var rmr = new ReceiveMessageRequest
                {
                    QueueUrl = queueUrl,
                    MaxNumberOfMessages = 10,
                    AttributeNames = new List<string> { "All" }
                };
            }
        }
    }

}
