using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using backend.Dtos.Property;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace backend.Services.PropertyServices
{
    public class PropertyServices : IPropertyServices
    {

        private static List<Property> properties = new List<Property> {
            new Property(),
            new Property
            {
                PropertyManager = new PropertyManager {Id = Guid.NewGuid(), Name = "HSB"},
                PropertyName = "BRF Sandalen",
                PropertyStreet = "Grönsaksvägen 99",
                PropertyCity = "Borås",
                PropertyPostalCode = "569 89",
                AdminName = "Gustavius Ybramsson",
                AdminPhoneNumber = "+46725005060",
                AdminEmail = "gustavius.ybramhsson@ica.se"
            },
            new Property
            {
                PropertyManager = new PropertyManager {Id = Guid.NewGuid(), Name = "HSB"},
                PropertyName = "BRF Ullfil",
                PropertyStreet = "Åsarytan 99",
                PropertyCity = "Borås",
                PropertyPostalCode = "500 56",
                AdminName = "Lena Lenartsson",
                AdminPhoneNumber = "+46785698532",
                AdminEmail = "lena..enartsson@gmail.com"
            },
            new Property
            {
                PropertyManager = new PropertyManager {Id = Guid.NewGuid(), Name = "Castellum"},
                PropertyName = "BRF Sandalen",
                PropertyStreet = "Grinoargatan 33",
                PropertyCity = "Borås",
                PropertyPostalCode = "515 25",
                AdminName = "Erik Eriksson",
                AdminPhoneNumber = "+46705289654",
                AdminEmail = "gunilla.svansson@castellum.com"
            }
        };
        public readonly IMapper _mapper;

        public PropertyServices(IMapper mapper)
        {
            _mapper = mapper;

        }

        public async Task<ServiceResponse<List<GetPropertyDto>>> GetAllProperties()
        {
            return new ServiceResponse<List<GetPropertyDto>>
            {
                Data = properties.Select(property => _mapper.Map<GetPropertyDto>(property)).ToList()
            };
        }

        public async Task<ServiceResponse<List<GetPropertyByPropertyManagerId>>> GetPropertiesPerPropertyManager(Guid id)
        {
            var property = properties.FirstOrDefault(property => property.PropertyManager.Id == id);
            if (property == null)
            {
                return new ServiceResponse<List<GetPropertyByPropertyManagerId>>
                {
                    Data = new List<GetPropertyByPropertyManagerId>()
                };
            }

            var propertyDto = _mapper.Map<GetPropertyByPropertyManagerId>(property);
            return new ServiceResponse<List<GetPropertyByPropertyManagerId>>
            {
                Data = new List<GetPropertyByPropertyManagerId> { propertyDto }
            };
        }
    }
}