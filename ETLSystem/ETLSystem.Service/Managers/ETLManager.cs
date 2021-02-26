using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ETLSystem.Service.DataAccess;
using ETLSystem.Service.Interfaces;
using ETLSystem.Service.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ETLSystem.Service.Managers
{
    public class ETLManager : IETLManager
    {
        private readonly IMapper mapper;
        private readonly IConfigManager configManager;

        public ETLManager(IMapper mapper, IConfigManager configManager)
        {
            this.mapper = mapper;
            this.configManager = configManager;
        }

        public async Task ProcessAsync(string body)
        {
            /* Extract
             * When we receive building data in any format, we should convert it to common format
             * (BuildingModel - includes fields of both format)
             */
            var buildingData = JsonConvert.DeserializeObject<BuildingModel>(body);


            /* Transform
             * We should convert BuildingModel object to Building onject
             */
            var updateBuilding = await CreateBuildingObject(buildingData);


            // We need internal id for store data as source independent id,
            // or We should put prefix to Id such as s1_123 or s2_123 - making this for create internal unique id


            /* HashWe can create hash data over few fields in request data(name + postcode, etc). and we can use it as id to make record algorithms help us in this case.(for creating internal unique id). (like md5 and it is efficient)
             * We can create hash data over few fields in request data(name + postcode, etc). and we can use it as id to make record
             * Then when we receive new request in other format , and we can create hash data over same datas and check it on db to see if it exists!
             */
            updateBuilding.Hash = await GetHashData(updateBuilding);


            /* Load
             *
             */
            await CreateOrUpdateBuildingAsync(updateBuilding);

        }

        public async Task<Building> CreateBuildingObject(BuildingModel buildingData)
        {
            var building = new Building();

            if (buildingData.Id > 0 && buildingData.Id.GetType() == typeof(int))
            {
                building.Id = buildingData.Id;
            }

            if (buildingData.Floorcount > 0 && buildingData.Floorcount.GetType() == typeof(int))
            {
                building.Floorcount = buildingData.Floorcount;
            }

            if (buildingData.Floorarea > 0 && buildingData.Floorarea.GetType() == typeof(int))
            {
                building.Floorarea = buildingData.Floorarea;
            }

            if (buildingData.Lat != 0 && buildingData.Lat.GetType() == typeof(double))
            {
                building.Lat = buildingData.Lat;
            }

            if (buildingData.Coordinates?.Length > 0 && buildingData.Coordinates[0].GetType() == typeof(double))
            {
                building.Lat = buildingData.Coordinates[0];
            }

            if (buildingData.Lon != 0 && buildingData.Lon.GetType() == typeof(double))
            {
                building.Lon = buildingData.Lon;
            }

            if (buildingData.Coordinates?.Length > 0 && buildingData.Coordinates[1].GetType() == typeof(double))
            {
                building.Lon = buildingData.Coordinates[1];
            }

            if (buildingData.Name?.GetType() == typeof(string))
            {
                building.Name = buildingData.Name;
            }

            if (buildingData.Address?.GetType() == typeof(string))
            {
                building.Address = buildingData.Address;
            }

            if ((buildingData.Address1?.GetType() == typeof(string)) ||
                 (buildingData.Address2?.GetType() == typeof(string)))
            {
                string[] address1 = buildingData.Address1.Split(", ");
                building.Name = address1[0];
                building.Address = String.Concat(address1[1], ", ",buildingData.Address2);
            }

            return building;
        }

        public async Task<Building> GetBuildingByHashAsync(string hash)
        {
            using (var dataContext = new DataContext(Options()))
            {
                return await dataContext.Building.FirstOrDefaultAsync(c => c.Hash == hash);
            }
        }

        public async Task<string> GetHashData(Building updateBuilding)
        {
            string sSourceData = String.Concat(updateBuilding.Name, updateBuilding.Address);

            byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData);
            byte[] hash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            string sHash = System.Text.Encoding.UTF8.GetString(hash);

            return sHash;
        }

        public async Task<int> CreateOrUpdateBuildingAsync(Building updateBuilding)
        {
            try
            {
                using (var dataContext = new DataContext(Options()))
                {

                    var building = await GetBuildingByHashAsync(updateBuilding.Hash);
                    if (building != null)
                    {
                        updateBuilding.Name = building.Name == null ? updateBuilding.Name : building.Name;
                        updateBuilding.Floorarea = building.Floorarea > 0 ? building.Floorarea : updateBuilding.Floorarea;
                        updateBuilding.Floorcount = building.Floorcount > 0 ? building.Floorcount : updateBuilding.Floorcount;
                        updateBuilding.Lat = building.Lat == 0.0 ? updateBuilding.Lat : building.Lat;
                        updateBuilding.Lon = building.Lon == 0.0 ? updateBuilding.Lon : building.Lon;

                        dataContext.Building.Update(updateBuilding);
                    }
                    else
                        await dataContext.Building.AddAsync(updateBuilding);

                    return await dataContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return 1;
        }

        public DbContextOptions<DataContext> Options()
        {
            if (configManager.TestConfig())
            {
                return new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("inMemoryCache").Options;
            }

            return new DbContextOptionsBuilder<DataContext>().UseNpgsql(configManager.GetDbConnectionString("ETL")).Options;
        }
    }
}
