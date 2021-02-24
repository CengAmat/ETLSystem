using System;
using System.Threading.Tasks;
using ETLSystem.Service.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace ETLSystem.Service.Interface
{
    public interface IETLManager
    {
        public Task ProcessAsync(string body, string group);
        public Task<Building> GetDataByIdAsync(Guid Id);
        public Task<int> CreateOrUpdateDataAsync(Building updateData);
        DbContextOptions<DataContext> Options();
    }
}
