using System;
using System.Threading;
using System.Threading.Tasks;
using ETLSystem.Listener;
using ETLSystem.Service;
using ETLSystem.Service.DataAccess;
using ETLSystem.Service.Interfaces;
using ETLSystem.Service.Models;
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
                 await etlManager.ProcessAsync(message);
             }
             catch (Exception ex)
             {
                Console.WriteLine(ex.ToString());
             }
             return true;
        }
    }
}
