using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ETLSystem.Listener
{
    public interface IListener
    {
        Task ExecuteAsync(CancellationToken stoppingToken);
    }

    public class Listener
    {
        private readonly IMessageHandler messageHandler;

        public Listener(IMessageHandler messageHandler)
        {
            this.messageHandler = messageHandler;
        }

        public async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine($"From Execute : {DateTime.UtcNow}");
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }

}
