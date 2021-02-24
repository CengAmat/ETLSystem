using System;
namespace ETLSystem.Service.Interface
{
    public interface IConfigManager
    {
        string GetDbConnectionString(string dbName);
    }
}
