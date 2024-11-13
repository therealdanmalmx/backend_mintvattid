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
        Task<ServiceResponse<List<GetPropertyByPropertyManagerId>>> GetPropertiesPerPropertyManager(Guid id);
        Task<ServiceResponse<List<GetPropertyDto>>> AddProperty(AddPropertyDto newProperty);
        Task<ServiceResponse<GetPropertyDto>> UpdateProperty(UpdatedPropertyDto updatedProperty);
        Task<ServiceResponse<List<GetPropertyDto>>> DeleteProperty(Guid id);

    }
}