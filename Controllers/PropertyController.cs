using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
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
            var claim = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            if (claim == null)
            {
                return Unauthorized(new ServiceResponse<GetPropertyDto> { Success = false, Message = "User claim not found" });
            }
            Guid userId = Guid.Parse(claim.Value);
            return Ok(await _propertyServices.GetAllProperties(userId));
        }


        [AllowAnonymous]
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<ServiceResponse<List<GetAllUsersForPropertyDto>>>> GetAllUsersForProperty(Guid propertyId)
        {
            return Ok(await _propertyServices.GetAllUsersForProperty(propertyId));
        }

        [AllowAnonymous]
        [HttpGet("GetAllWashrooms")]
        public async Task<ActionResult<ServiceResponse<List<GetWashroomsPerPropertyDto>>>> GetWashroomsPerProperty(Guid propertyId)
        {
            return Ok(await _propertyServices.GetWashroomsPerProperty(propertyId));
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