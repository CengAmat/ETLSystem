using ETLSystem.Service.DataAccess;
using ETLSystem.Test.Base;
using ETLSystem.Test.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ETLSystem.Test.xUnitTest
{
    public class ETLManagerTests : TestBase
    {
        #region CreateBuildingTest
        [Fact]
        public async Task CreateBuilding_Return_Success()
        {
            var sBuildingId = SessionHelper.DummyBuilding.Id;

            var updateBuilding = new Building()
            {
                Id = sBuildingId,
                Name = "The Shard",
                Address = "32 London Bridge St London SE1 9SG",
                Lat = -0.0865,
                Lon = 51.5045,
                Floorcount = 95,
                Floorarea = 0
            };

            await _etlManager.CreateOrUpdateBuildingAsync(updateBuilding);
            var createdBuilding = await context.Building.Where(u => u.Id == _requestBuilding.Id).SingleOrDefaultAsync();

            Assert.Equal(updateBuilding.Name, createdBuilding.Name);
            Assert.Equal(updateBuilding.Address, createdBuilding.Address);
        }
        #endregion
    }
}
