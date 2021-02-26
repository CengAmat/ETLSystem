using System;
using AutoMapper;
using ETLSystem.Service.DataAccess;
using ETLSystem.Service.Models;

namespace ETLSystem.Test
{
    public class DatabaseTest
    {
    }

    public class TestAutoMapperProfile : Profile
    {
        public TestAutoMapperProfile()
        {
            CreateMap<BuildingModel, Building>();
        }
    }
}
