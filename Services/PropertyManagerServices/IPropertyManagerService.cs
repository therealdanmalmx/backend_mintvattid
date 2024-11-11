using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.PropertyManager;
using backend.models;

namespace backend.Services.PropertyManagerServices
{
    public interface IPropertyManagerService
    {
        Task<ServiceResponse<List<GetPropertyManagerDto>>> GetAllPropertyManager();
        Task<ServiceResponse<GetPropertyManagerDto>> GetPropertyManagerById(Guid id);
        Task<ServiceResponse<List<GetPropertyManagerDto>>> AddPropertyManager(AddPropertyManagerDto newPropertyManager);

        Task<ServiceResponse<GetPropertyManagerDto>> UpdatePropertyManager(UpdatePropertyManagerDto updatedPropertyManager);
        // Task<ServiceResponse<List<GetPropertyManagerDto>>> DeletePropertyManagerById(Guid id);
    }
}