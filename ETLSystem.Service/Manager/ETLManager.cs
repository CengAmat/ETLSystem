using System;
using System.Threading.Tasks;
using AutoMapper;
using ETLSystem.Service.Database;
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

        public async Task ProcessAsync(string body, string group)
        {
            // VALIDATE

            // PROCESS

            // SAVE TO DB
        }

        public async Task<DataS1> GetDataByIdAsync(Guid Id)
        {
            using (var dataContext = new DataContext(Options()))
            {
                return await dataContext.DataS1.FirstOrDefaultAsync(c => c.Id == Id);
            }
        }


        public async Task<int> CreateOrUpdateDataAsync(DataS1 updateData)
        {
            using (var dataContext = new DataContext(Options()))
            {

                var data = await GetDataByIdAsync(updateData.Id);
                if (data != null)
                    dataContext.Entry(data).CurrentValues.SetValues(updateData);
                else
                    await dataContext.DataS1.AddAsync(updateData);

                return await dataContext.SaveChangesAsync();
            }
        }

        public DbContextOptions<DataContext> Options()
        {
            return new DbContextOptionsBuilder<DataContext>().UseNpgsql(configManager.GetDbConnectionString("ETL")).Options;
        }
    }
}
