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
using backend.Dtos.WashRoom;
using backend.Dtos.WashTime;

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
            CreateMap<User, GetAllUsersForPropertyDto>();
            CreateMap<User, GetUserDto>();

            // Property
            CreateMap<Property, GetPropertyDto>();
            CreateMap<Property, GetPropertyByPropertyManagerIdDto>();
            CreateMap<AddPropertyDto, Property>();
            CreateMap<UpdatedPropertyDto, Property>();

            // WasRoom
            CreateMap<WashRoom, GetWashRoomsDto>();
            CreateMap<WashRoom, GetWashroomsPerPropertyDto>();
            CreateMap<AddWashroomDto, WashRoom>();
            CreateMap<UpdateWashroomDto, WashRoom>();

            // WashTime
            CreateMap<WashTime, GetWashTimesDto>();
            CreateMap<AddWasTimesDto, WashTime>();
            CreateMap<UpdatedWashTimeDto, WashTime>();

            // PropertyManager
            CreateMap<PropertyManager, GetPropertyManagerDto>();
            CreateMap<AddPropertyManagerDto, PropertyManager>();
            CreateMap<UpdatePropertyManagerDto, PropertyManager>();
        }
    }
}