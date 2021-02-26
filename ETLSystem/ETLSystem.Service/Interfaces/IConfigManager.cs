
namespace ETLSystem.Service.Interfaces
{
    public interface IConfigManager
    {
        string GetDbConnectionString(string dbName);
        bool TestConfig();
    }
}
