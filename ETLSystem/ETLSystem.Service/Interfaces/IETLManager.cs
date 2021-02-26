using System;
using System.Threading.Tasks;
using ETLSystem.Service.DataAccess;
using ETLSystem.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace ETLSystem.Service.Interfaces
{
    public interface IETLManager
    {
        public Task ProcessAsync(string body);
        public Task<Building> GetBuildingByHashAsync(string hash);
        public Task<int> CreateOrUpdateBuildingAsync(Building updateData);
        public Task<string> GetHashData(Building updateBuilding);
        public Task<Building> CreateBuildingObject(BuildingModel buildingData);
        DbContextOptions<DataContext> Options();
    }
}
