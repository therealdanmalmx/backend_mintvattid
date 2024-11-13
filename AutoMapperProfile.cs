using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using backend.Dtos.Property;
using backend.Dtos.PropertyManager;

namespace backend
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // PropertyManager
            CreateMap<PropertyManager, GetPropertyManagerDto>();
            CreateMap<AddPropertyManagerDto, PropertyManager>();
            CreateMap<UpdatePropertyManagerDto, PropertyManager>();

            // Property
            CreateMap<Property, GetPropertyDto>();
            CreateMap<Property, GetPropertyByPropertyManagerId>();
            CreateMap<AddPropertyDto, Property>();
        }
    }
}