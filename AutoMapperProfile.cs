using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using backend.Dtos.PropertyManager;

namespace backend
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PropertyManager, GetPropertyManagerDto>();
            CreateMap<AddPropertyManagerDto, PropertyManager>();
            CreateMap<UpdatePropertyManagerDto, PropertyManager>();
        }
    }
}