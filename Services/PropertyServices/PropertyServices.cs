using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using backend.Data;
using backend.Dtos.Property;
using backend.Dtos.UserDto;
using backend.Dtos.WashRoom;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.PropertyServices
{
    public class PropertyServices : IPropertyServices
    {
        private readonly DataContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public readonly IMapper _mapper;

        public PropertyServices(IMapper mapper, DataContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<List<GetPropertyDto>>> AddProperty(AddPropertyDto newProperty)
        {
            var serviceResponse = new ServiceResponse<List<GetPropertyDto>>();
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Användaren är inte autentiserad";
            }

            var property = _mapper.Map<Property>(newProperty);
            property.UserId = Guid.Parse(userId);

            _db.Properties.Add(property);
            await _db.SaveChangesAsync();

            var properties = await _db.Properties
                .Where(p => p.UserId.Equals(userId))
                .ProjectTo<GetPropertyDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            serviceResponse.Data = properties;

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetPropertyDto>>> GetAllProperties(Guid userId)
        {
            var serviceResponse = new ServiceResponse<GetPropertyDto>();
            var dbProperties = await _db.Properties.ToListAsync();

            return new ServiceResponse<List<GetPropertyDto>>
            {
                Data = dbProperties.Select(property => _mapper.Map<GetPropertyDto>(property)).ToList()
            };
        }

        public async Task<ServiceResponse<List<GetAllUsersForPropertyDto>>> GetAllUsersForProperty(Guid propertyId)
        {
            var serviceResponse = new ServiceResponse<List<GetAllUsersForPropertyDto>>();

            try
            {
                var property = await _db.Properties.Include(p => p.Users).FirstOrDefaultAsync(p => p.Id == propertyId);

                if (property == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Fastigheten hittades inte";
                    return serviceResponse;
                }

                var UsersForProperty = await _db.Users
                    .Where(u => u.PropertyId == propertyId)
                    .GroupBy(u => u.Property.PropertyName)
                    .Select(group => new GetAllUsersForPropertyDto
                    {
                        PropertyName = group.Key,
                        Users = group.Select(u => _mapper.Map<GetUserDto>(u)).ToList()
                    })
                    .ToListAsync();

                return new ServiceResponse<List<GetAllUsersForPropertyDto>>
                {
                    Data = UsersForProperty,
                };
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }


            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetPropertyByPropertyManagerIdDto>>> GetPropertiesPerPropertyManager(Guid id)
        {
            var serviceResponse = new ServiceResponse<List<GetPropertyByPropertyManagerIdDto>>();
            var dbProperties = await _db.Properties
            .Where(property => property.Id == id)
            .ToListAsync();

            serviceResponse.Data = dbProperties.Select(p => _mapper.Map<GetPropertyByPropertyManagerIdDto>(p)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPropertyDto>> UpdateProperty(UpdatedPropertyDto updatedProperty)
        {
            ServiceResponse<GetPropertyDto> serviceResponse = new ServiceResponse<GetPropertyDto>();

            Property property = await _db.Properties
                .FirstOrDefaultAsync(property => property.Id.Equals(updatedProperty.Id));
            try
            {
                if (property != null)
                {
                    if (!string.IsNullOrEmpty(updatedProperty.PropertyName))
                    {
                        property.PropertyName = updatedProperty.PropertyName;
                    }
                    if (!string.IsNullOrEmpty(updatedProperty.PropertyStreet))
                    {
                        property.PropertyStreet = updatedProperty.PropertyStreet;
                    }
                    if (!string.IsNullOrEmpty(updatedProperty.PropertyCity))
                    {
                        property.PropertyCity = updatedProperty.PropertyCity;
                    }
                    if (!string.IsNullOrEmpty(updatedProperty.PropertyPostalCode))
                    {
                        property.PropertyPostalCode = updatedProperty.PropertyPostalCode;
                    }

                    await _db.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetPropertyDto>(property);

                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Property not found";
                }

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }


            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetPropertyDto>>> DeleteProperty(Guid id)
        {
            ServiceResponse<List<GetPropertyDto>> serviceResponse = new ServiceResponse<List<GetPropertyDto>>();

            try
            {
                Property property = await _db.Properties.FirstAsync(property => property.Id.Equals(id));
                _db.Properties.Remove(property);
                await _db.SaveChangesAsync();
                serviceResponse.Data = _db.Properties.Select(property => _mapper.Map<GetPropertyDto>(property)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetWashroomsPerPropertyDto>>> GetWashroomsPerProperty(Guid propertyId)
        {
            {
                var serviceResponse = new ServiceResponse<List<GetWashroomsPerPropertyDto>>();

                try
                {
                    var property = await _db.Properties.Include(p => p.Washrooms).FirstOrDefaultAsync(p => p.Id == propertyId);

                    if (property == null)
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message = "Fastigheten hittades inte";
                        return serviceResponse;
                    }

                    var washroomsForProperty = await _db.Washrooms
                        .Where(w => w.PropertyId == propertyId)
                        .GroupBy(w => w.Property.PropertyName)
                        .Select(group => new GetWashroomsPerPropertyDto
                        {
                            PropertyName = group.Key,
                            WashRooms = group.Select(u => _mapper.Map<GetWashRoomsDto>(u)).ToList()
                        })
                        .ToListAsync();

                    serviceResponse.Data = washroomsForProperty;


                }
                catch (Exception ex)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = ex.Message;
                }


                return serviceResponse;
            }
        }
    }
}