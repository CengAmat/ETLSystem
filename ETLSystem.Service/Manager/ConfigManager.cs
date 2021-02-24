using System;
using ETLSystem.Service.Interface;
using Microsoft.Extensions.Configuration;

namespace ETLSystem.Service.Manager
{
    public class ConfigManager : IConfigManager
    {
        private readonly IConfiguration configuration;

        public ConfigManager(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetDbConnectionString(string dbName)
        {
            string key = $"ConnectionString:{dbName}";
            return configuration[key];
        }
    }
}
