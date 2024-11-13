using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using backend.Data;
using backend.Dtos.PropertyManager;
using backend.models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.PropertyManagerServices
{
    public class PropertyManagerService : IPropertyManagerService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _db;

        public PropertyManagerService(IMapper mapper, DataContext db)
        {
            _mapper = mapper;
            _db = db;
        }
        public async Task<ServiceResponse<List<GetPropertyManagerDto>>> GetAllPropertyManager()
        {
            var serviceResponse = new ServiceResponse<GetPropertyManagerDto>();
            var dbProjectManager = await _db.PropertyManagers.ToListAsync();
            return new ServiceResponse<List<GetPropertyManagerDto>>
            {
                Data = dbProjectManager.Select(propertyManager => _mapper.Map<GetPropertyManagerDto>(propertyManager)).ToList()
            };
        }

        public async Task<ServiceResponse<GetPropertyManagerDto>> GetPropertyManagerById(Guid id)
        {

            var serviceResponse = new ServiceResponse<GetPropertyManagerDto>();
            var dBPropertyManager = await _db.PropertyManagers.FirstOrDefaultAsync(propertyManager => propertyManager.Id == id);
            serviceResponse.Data = _mapper.Map<GetPropertyManagerDto>(dBPropertyManager);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetPropertyManagerDto>>> AddPropertyManager(AddPropertyManagerDto newPropertyManager)
        {
            var serviceResponse = new ServiceResponse<List<GetPropertyManagerDto>>();
            _db.PropertyManagers.Add(_mapper.Map<PropertyManager>(newPropertyManager));
            await _db.SaveChangesAsync();
            serviceResponse.Data = await _db.PropertyManagers.Select(propertyManager => _mapper.Map<GetPropertyManagerDto>(propertyManager)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPropertyManagerDto>> UpdatePropertyManager(UpdatePropertyManagerDto updatedPropertyManager)
        {
            ServiceResponse<GetPropertyManagerDto> serviceResponse = new ServiceResponse<GetPropertyManagerDto>();

            try
            {
                PropertyManager propertyManager = await _db.PropertyManagers
                .FirstOrDefaultAsync(propertyManager => propertyManager.Id == updatedPropertyManager.Id);
                if (propertyManager != null)
                {
                    if (!string.IsNullOrEmpty(updatedPropertyManager.Name))
                    {
                        propertyManager.Name = updatedPropertyManager.Name;
                    }
                    if (!string.IsNullOrEmpty(updatedPropertyManager.StreetName))
                    {
                        propertyManager.StreetName = updatedPropertyManager.StreetName;
                    }
                    if (!string.IsNullOrEmpty(updatedPropertyManager.City))
                    {
                        propertyManager.City = updatedPropertyManager.City;
                    }
                    if (!string.IsNullOrEmpty(updatedPropertyManager.PostalCode))
                    {
                        propertyManager.PostalCode = updatedPropertyManager.PostalCode;
                    }
                    if (!string.IsNullOrEmpty(updatedPropertyManager.AdminName))
                    {
                        propertyManager.AdminName = updatedPropertyManager.AdminName;
                    }
                    if (!string.IsNullOrEmpty(updatedPropertyManager.AdminPhoneNumber))
                    {
                        propertyManager.AdminPhoneNumber = updatedPropertyManager.AdminPhoneNumber;
                    }
                    if (!string.IsNullOrEmpty(updatedPropertyManager.AdminEmail))
                    {
                        propertyManager.AdminEmail = updatedPropertyManager.AdminEmail;
                    }

                    await _db.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetPropertyManagerDto>(propertyManager);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Property Manager not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;

        }

        public async Task<ServiceResponse<List<GetPropertyManagerDto>>> DeletePropertyManager(Guid id)
        {
            ServiceResponse<List<GetPropertyManagerDto>> serviceResponse = new ServiceResponse<List<GetPropertyManagerDto>>();

            try
            {
                PropertyManager propertyManager = await _db.PropertyManagers.FirstAsync(propertyManager => propertyManager.Id == id);

                _db.PropertyManagers.Remove(propertyManager);
                await _db.SaveChangesAsync();
                serviceResponse.Data = _db.PropertyManagers.Select(propertyManager => _mapper.Map<GetPropertyManagerDto>(propertyManager)).ToList();
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