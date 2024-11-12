using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using backend.Dtos.PropertyManager;
using backend.models;

namespace backend.Services.PropertyManagerServices
{
    public class PropertyManagerService : IPropertyManagerService
    {
        private static List<PropertyManager> propertyManagers = new List<PropertyManager> {
            new PropertyManager(),
            new PropertyManager {
                Name = "Riksbyggen",
                StreetName = "Höstgatan 587",
                City = "Borås",
                PostalCode = "505 05",
                AdminName = "Albert Ygvesson",
                AdminPhoneNumber = "+46335986532",
                AdminEmail = "albert.ygvesson@riksbyggen.se"
            },
            new PropertyManager {
                Name = "Castellum",
                StreetName = "Grinoargatan 33",
                City = "Borås",
                PostalCode = "515 25",
                AdminName = "Gunilla Svansson",
                AdminPhoneNumber = "+46705289654",
                AdminEmail = "gunilla.svansson@castellum.com"
            }
        };
        private readonly IMapper _mapper;
        public PropertyManagerService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetPropertyManagerDto>>> GetAllPropertyManager()
        {
            return new ServiceResponse<List<GetPropertyManagerDto>>
            {
                Data = propertyManagers.Select(propertyManager => _mapper.Map<GetPropertyManagerDto>(propertyManager)).ToList()
            };
        }

        public async Task<ServiceResponse<GetPropertyManagerDto>> GetPropertyManagerById(Guid id)
        {

            var serviceResponse = new ServiceResponse<GetPropertyManagerDto>();
            var propertyManager = propertyManagers.FirstOrDefault(propertyManager => propertyManager.Id == id);
            serviceResponse.Data = _mapper.Map<GetPropertyManagerDto>(propertyManager);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetPropertyManagerDto>>> AddPropertyManager(AddPropertyManagerDto newPropertyManager)
        {
            var serviceResponse = new ServiceResponse<List<GetPropertyManagerDto>>();
            propertyManagers.Add(_mapper.Map<PropertyManager>(newPropertyManager));
            serviceResponse.Data = propertyManagers.Select(propertyManager => _mapper.Map<GetPropertyManagerDto>(propertyManager)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPropertyManagerDto>> UpdatePropertyManager(UpdatePropertyManagerDto updatedPropertyManager)
        {
            ServiceResponse<GetPropertyManagerDto> serviceResponse = new ServiceResponse<GetPropertyManagerDto>();

            try
            {
                PropertyManager propertyManager = propertyManagers.FirstOrDefault(propertyManager => propertyManager.Id == updatedPropertyManager.Id);
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
                PropertyManager propertyManager = propertyManagers.First(propertyManager => propertyManager.Id == id);

                propertyManagers.Remove(propertyManager);
                serviceResponse.Data = propertyManagers.Select(propertyManager => _mapper.Map<GetPropertyManagerDto>(propertyManager)).ToList();
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