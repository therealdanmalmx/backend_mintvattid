using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using backend.Data;
using backend.Dtos.Property;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.PropertyServices
{
    public class PropertyServices : IPropertyServices
    {
        private readonly DataContext _db;
        public readonly IMapper _mapper;

        public PropertyServices(IMapper mapper, DataContext db)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetPropertyDto>>> AddProperty(AddPropertyDto newProperty, Guid userId)
        {
            var serviceResponse = new ServiceResponse<List<GetPropertyDto>>();
            var property = _mapper.Map<Property>(newProperty);
            if (property.User != null)
            {
                property.User.Id = userId;
            }
            _db.Properties.Add(property);
            await _db.SaveChangesAsync();
            serviceResponse.Data = _db.Properties
                .Where(p => p.User != null && p.User.Id.Equals(userId))
                .Select(property => _mapper.Map<GetPropertyDto>(property))
                .ToList();

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
                var dbProperties = await _db.Properties.(p => p.User.Id).FirstOrDefaultAsync(p => p.Id.Equals(propertyId));
                if (dbProperties?.User == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Användare hittades inte för den angivna fastigheten";
                    return serviceResponse;
                }

                if (dbProperties == null || dbProperties.Id == Guid.Empty)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Du måste ange en giltig fastighet";
                    return serviceResponse;
                }

                var users = await _db.Users.Where(u => u.PropertyId == propertyId).Select(u => _mapper.Map<GetAllUsersForPropertyDto>(u)).ToListAsync();

                serviceResponse.Data = users;
                serviceResponse.Success = true;

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            serviceResponse.Data = _db.Properties.Select(property => _mapper.Map<GetAllUsersForPropertyDto>(property)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetPropertyByPropertyManagerIdDto>>> GetPropertiesPerPropertyManager(Guid id)
        {
            var serviceResponse = new ServiceResponse<List<GetPropertyByPropertyManagerIdDto>>();
            var dbProperties = await _db.Properties
            .Where(property => property.Id == id)
            .ToListAsync();

            serviceResponse.Data = dbProperties.Select(properties => _mapper.Map<GetPropertyByPropertyManagerIdDto>(properties)).ToList();
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

    }
}