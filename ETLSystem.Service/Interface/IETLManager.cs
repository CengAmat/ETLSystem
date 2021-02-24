using System;
using System.Threading.Tasks;
using ETLSystem.Service.Database;
using Microsoft.EntityFrameworkCore;

namespace ETLSystem.Service.Interface
{
    public interface IETLManager
    {
        public Task ProcessAsync(string body, string group);
        public Task<DataS1> GetDataByIdAsync(Guid Id);
        public Task<int> CreateOrUpdateDataAsync(DataS1 updateData);
        DbContextOptions<DataContext> Options();
    }
}
