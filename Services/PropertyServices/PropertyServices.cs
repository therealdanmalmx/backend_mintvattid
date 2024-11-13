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

        public async Task<ServiceResponse<List<GetPropertyDto>>> AddProperty(AddPropertyDto newProperty)
        {
            var serviceResponse = new ServiceResponse<List<GetPropertyDto>>();
            _db.Properties.Add(_mapper.Map<Property>(newProperty));
            await _db.SaveChangesAsync();
            serviceResponse.Data = await _db.Properties.Select(property => _mapper.Map<GetPropertyDto>(property)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetPropertyDto>>> GetAllProperties()
        {
            var serviceResponse = new ServiceResponse<GetPropertyDto>();
            var dbProperty = await _db.Properties.ToListAsync();

            return new ServiceResponse<List<GetPropertyDto>>
            {
                Data = dbProperty.Select(property => _mapper.Map<GetPropertyDto>(property)).ToList()
            };
        }

        public async Task<ServiceResponse<List<GetPropertyByPropertyManagerId>>> GetPropertiesPerPropertyManager(Guid id)
        {
            var serviceResponse = new ServiceResponse<List<GetPropertyByPropertyManagerId>>();
            var dbProperties = await _db.Properties
            .Where(property => property.PropertyManagerId == id)
            .ToListAsync();

            serviceResponse.Data = dbProperties.Select(properties => _mapper.Map<GetPropertyByPropertyManagerId>(properties)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPropertyDto>> UpdateProperty(UpdatedPropertyDto updatedProperty)
        {
            ServiceResponse<GetPropertyDto> serviceResponse = new ServiceResponse<GetPropertyDto>();

            Property property = await _db.Properties
                .FirstOrDefaultAsync(property => property.Id == updatedProperty.Id);
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
                    if (!string.IsNullOrEmpty(updatedProperty.AdminName))
                    {
                        property.AdminName = updatedProperty.AdminName;
                    }
                    if (!string.IsNullOrEmpty(updatedProperty.AdminPhoneNumber))
                    {
                        property.AdminPhoneNumber = updatedProperty.AdminPhoneNumber;
                    }
                    if (!string.IsNullOrEmpty(updatedProperty.AdminEmail))
                    {
                        property.AdminEmail = updatedProperty.AdminEmail;
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
                Property property = await _db.Properties.FirstAsync(property => property.Id == id);
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