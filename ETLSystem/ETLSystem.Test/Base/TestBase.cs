using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ETLSystem.Service.DataAccess;
using ETLSystem.Service.Interfaces;
using ETLSystem.Service.Managers;
using ETLSystem.Service.Models;
using ETLSystem.Test.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ETLSystem.Test.Base
{
    public class TestBase
    {
        static IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        protected DataContext context;
        protected IETLManager _etlManager;
        protected Mapper _mapper;
        protected BuildingModel _requestBuilding;

        public TestBase()
        {
            Builder();
            SetDummyBuilding();
            MapRequestBuilding();
        }

        public void Builder()
        {
            var _options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("inMemoryCache").Options;
            context = new DataContext(_options);
            var autoMapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<TestAutoMapperProfile>());
            IConfigManager configManager = new ConfigManager(config);
            _mapper = new Mapper(autoMapperConfig);
            _etlManager = new ETLManager(_mapper, configManager);

            try
            {
                ContextInitializer.SeedData(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task SetDummyBuilding()
        {
            SessionHelper.DummyBuilding = await context.Building.Where(u => u.Id == 1234).SingleOrDefaultAsync();
        }

        public void MapRequestBuilding()
        {
            _requestBuilding = _mapper.Map<BuildingModel>(SessionHelper.DummyBuilding);
        }
    }
}
