using System;
using AutoMapper;
using ETLSystem.Service.DataAccess;
using ETLSystem.Service.Models;

namespace ETLSystem.Listener
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Building, BuildingModel>();
            CreateMap<BuildingModel, Building>();

            CreateMap<Building, S1>();
            CreateMap<S1, Building>();

            CreateMap<Building, S2>();
            CreateMap<S2, Building>();
        }
    }
}
