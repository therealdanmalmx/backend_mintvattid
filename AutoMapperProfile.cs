using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using backend.Dtos.Property;
using backend.Dtos.PropertyManager;
using backend.Dtos.RealEstateCompany;
using backend.Dtos.UserDto;
using backend.Dtos.UserRegister;

namespace backend
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // RealEstateCompany
            CreateMap<RealEstateCompany, GetRealEstateCompanyDto>();
            CreateMap<AddRealEstateCompanyDto, RealEstateCompany>();

            // User
            CreateMap<User, GetUserDto>();
            CreateMap<UserRegisterDto, User>();

            // Property
            CreateMap<Property, GetPropertyDto>();
            CreateMap<User, GetAllUsersForPropertyDto>()
                .ForMember(dest => dest.PropertyName, opt => opt.MapFrom(src => src.Property.PropertyName))
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => new List<User> { src }));
            CreateMap<User, GetUserDto>();
            CreateMap<Property, GetPropertyByPropertyManagerIdDto>();
            CreateMap<AddPropertyDto, Property>();
            CreateMap<UpdatedPropertyDto, Property>();

            // PropertyManager
            CreateMap<PropertyManager, GetPropertyManagerDto>();
            CreateMap<AddPropertyManagerDto, PropertyManager>();
            CreateMap<UpdatePropertyManagerDto, PropertyManager>();
        }
    }
}