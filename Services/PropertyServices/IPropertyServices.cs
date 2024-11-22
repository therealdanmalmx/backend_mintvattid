using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.Property;

namespace backend.Services.PropertyServices
{
    public interface IPropertyServices
    {
        Task<ServiceResponse<List<GetPropertyDto>>> GetAllProperties();
        Task<ServiceResponse<List<GetPropertyByPropertyManagerIdDto>>> GetPropertiesPerPropertyManager(Guid id);
        Task<ServiceResponse<List<GetAllUsersForPropertyDto>>> GetAllUsersForProperty(Guid propertyId);
        Task<ServiceResponse<List<GetWashroomsPerPropertyDto>>> GetWashroomsPerProperty(Guid propertyId);
        Task<ServiceResponse<List<GetPropertyDto>>> AddProperty(AddPropertyDto newProperty);
        Task<ServiceResponse<GetPropertyDto>> UpdateProperty(UpdatedPropertyDto updatedProperty, Guid propertyId);
        Task<ServiceResponse<List<GetPropertyDto>>> DeleteProperty(Guid id);

    }
}