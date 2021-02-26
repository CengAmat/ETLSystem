using System;
using ETLSystem.Service.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ETLSystem.Service.Managers
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

        public bool TestConfig()
        {
            bool result = false;
            string key = "TestConfig";
            bool.TryParse(configuration[key], out result);
            return result;
        }
    }
}
