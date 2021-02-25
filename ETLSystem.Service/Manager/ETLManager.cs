using System;
using System.Threading.Tasks;
using AutoMapper;
using ETLSystem.Service.DataAccess;
using ETLSystem.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace ETLSystem.Service.Manager
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
            // VALIDATE

            /* We should validate json data here
             * 
             * We should check if id is integer
             * 
             * We should check if this building record exists
             */

            // EXTRACT

            /* Getting datas from S3 and put InMemory array!
             * 
             */

            // rawJsonData { source: "D2", and other fields in jsondata }

            // const { id, address1, address2, lat, lon, floorcount, floorarea, name, postcode, coordinates } = rawJsonData

            // const validFields = {}; empty object for not to block old field value with new incorrect format field value

            //if (id && typeof id === “number”) {
            //    validField.id = id;
            //}

            //if (address && typeof address === “string”) {
            //    validField.address = address;
            //}

            //if (floorarea && typeof floorarea === “number”) {
            //    validField.floorarea = floorarea;
            //} and so on ...

            // We need internal id for store data as source independent id,
            // or We should put prefix to Id such as s1_123 or s2_123 - making this for create internal unique id

            // Also hash algorithms help us in this case.(for internal unique id). (like md5 and it is efficient)
            // We can create hash data over few fields in request data(name + postcode, etc). and we can use it as id to make record unique
            // Then we receive new request in other format , and we can create hash data over same datas and check it on db to see if it exists!

            // SAVE TO DB

            //CreateOrUpdateDataAsync(buildingData);
        }

        public async Task<Building> GetDataByIdAsync(Guid Id)
        {
            using (var dataContext = new DataContext(Options()))
            {
                return await dataContext.Building.FirstOrDefaultAsync(c => c.Id == Id);
            }
        }


        public async Task<int> CreateOrUpdateDataAsync(Building updateData)
        {
            using (var dataContext = new DataContext(Options()))
            {

                var data = await GetDataByIdAsync(updateData.Id);
                if (data != null)
                    dataContext.Entry(data).CurrentValues.SetValues(updateData);
                else
                    await dataContext.Building.AddAsync(updateData);

                return await dataContext.SaveChangesAsync();
            }
        }

        public DbContextOptions<DataContext> Options()
        {
            return new DbContextOptionsBuilder<DataContext>().UseNpgsql(configManager.GetDbConnectionString("ETL")).Options;
        }
    }
}
