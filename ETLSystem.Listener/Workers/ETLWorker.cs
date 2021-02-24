using System;
using System.Threading;
using System.Threading.Tasks;
using ETLSystem.Listener;
using ETLSystem.Service;
using ETLSystem.Service.Interfaces;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace ETLSystem.Listener.Workers
{
    public class ETLWorker : BackgroundService
    {
        private readonly IConfigManager configManager;
        private readonly IETLManager etlManager;

        public ETLWorker(IConfigManager configManager, IETLManager etlManager)
        {
            this.configManager = configManager;
            this.etlManager = etlManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IMessageHandler messageHandler = new ETLHandler(etlManager);

            var listener = new Listener(messageHandler);
            await listener.ExecuteAsync(stoppingToken);
        }
    }

    public class ETLHandler : IMessageHandler
    {
        private readonly IETLManager etlManager;

        public ETLHandler(IETLManager etlManager)
        {
            this.etlManager = etlManager;
        }

        public async Task<bool> ExecuteAsync(string message)
        {
             try
             {
                 var IsMessageGroup = message.Attributes.TryGetValue("MessageGroupId", out string messageGroup);
                 await etlManager.ProcessAsync(message.Body, messageGroup);
             }
             catch (Exception ex)
             {

             }
             return true;
        }
    }
}
