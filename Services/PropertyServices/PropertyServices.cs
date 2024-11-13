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
        private readonly DataContext _context;

        public PropertyServices(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetPropertyDto>>> GetAllProperties()
        {
            var serviceResponse = new ServiceResponse<GetPropertyDto>();
            var dbProperty = await _context.Properties.ToListAsync();

            return new ServiceResponse<List<GetPropertyDto>>
            {
                Data = dbProperty.Select(property => _mapper.Map<GetPropertyDto>(property)).ToList()
            };
        }

        public async Task<ServiceResponse<List<GetPropertyByPropertyManagerId>>> GetPropertiesPerPropertyManager(Guid id)
        {
            var serviceResponse = new ServiceResponse<List<GetPropertyByPropertyManagerId>>();
            var dbProperties = await _context.Properties
            .Where(property => property.PropertyManager.Id == id)
            .ToListAsync();

            serviceResponse.Data = dbProperties.Select(properties => _mapper.Map<GetPropertyByPropertyManagerId>(properties)).ToList();
            return serviceResponse;
        }
    }
}