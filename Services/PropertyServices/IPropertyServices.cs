using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.Property;

namespace backend.Services.PropertyServices
{
    public interface IPropertyServices
    {
        Task<ServiceResponse<List<GetPropertyDto>>> GetAllProperties(Guid userId);
        Task<ServiceResponse<List<GetPropertyByPropertyManagerIdDto>>> GetPropertiesPerPropertyManager(Guid id);
        Task<ServiceResponse<List<GetAllUsersForPropertyDto>>> GetAllUsersForProperty(Guid propertyIdd);
        Task<ServiceResponse<List<GetWashroomsPerPropertyDto>>> GetWashroomsPerProperty(Guid propertyIdd);
        Task<ServiceResponse<List<GetPropertyDto>>> AddProperty(AddPropertyDto newProperty);
        Task<ServiceResponse<GetPropertyDto>> UpdateProperty(UpdatedPropertyDto updatedProperty);
        Task<ServiceResponse<List<GetPropertyDto>>> DeleteProperty(Guid id);

    }
}