using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.Property;
using backend.Services.PropertyServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PropertyController : Controller
    {
        private readonly ILogger<PropertyController> _logger;
        private readonly IPropertyServices _propertyServices;

        public PropertyController(ILogger<PropertyController> logger, IPropertyServices propertyServices)
        {
            _propertyServices = propertyServices;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<GetPropertyDto>>> GetAllProperties()
        {
            return Ok(await _propertyServices.GetAllProperties());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetPropertyDto>>>> GetPropertiesPerPropertyManager(Guid id)
        {
            return Ok(await _propertyServices.GetPropertiesPerPropertyManager(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetPropertyDto>>>> AddProperty(AddPropertyDto newProperty)
        {
            return Ok(await _propertyServices.AddProperty(newProperty));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetPropertyDto>>>> UpdateProperty(UpdatedPropertyDto updatedProperty)
        {
            return Ok(await _propertyServices.UpdateProperty(updatedProperty));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetPropertyDto>>>> DeleteProperty(Guid id)
        {
            return Ok(await _propertyServices.DeleteProperty(id));
        }
    }
}