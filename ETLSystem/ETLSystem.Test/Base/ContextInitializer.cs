using Bogus;
using ETLSystem.Service.DataAccess;
using ETLSystem.Service.Models;
using System;

namespace ETLSystem.Test.Base
{
    public class ContextInitializer
    {
        public static void SeedData(DataContext context)
        {
            var fakeBuilding1 = new Faker<Building>()
                .RuleFor(u => u.Id, f => 1234)
                .RuleFor(u => u.Name, f => "The Shard")
                .RuleFor(u => u.Address, (f, u) => "32 London Bridge St London SE1 9SG")
                .RuleFor(u => u.Lat, (f, u) => -0.0865)
                .RuleFor(u => u.Lon, (f, u) => 51.5045)
                .RuleFor(u => u.Floorcount, f => 95)
                .RuleFor(o => o.Floorarea, f => 0);

            context.Building.AddRange(fakeBuilding1);

            var fakeBuilding2 = new Faker<Building>()
                .RuleFor(u => u.Id, f => 2345)
                .RuleFor(u => u.Name, f => "The Gherkin")
                .RuleFor(u => u.Address, (f, u) => "30 St Mary Axe, London EC3A 8EP")
                .RuleFor(u => u.Lat, (f, u) => 0.0803)
                .RuleFor(u => u.Lon, (f, u) => 51.5145)
                .RuleFor(u => u.Floorcount, f => 0)
                .RuleFor(o => o.Floorarea, f => 47950);

            context.Building.AddRange(fakeBuilding2);

            context.SaveChanges();
        }
    }
}