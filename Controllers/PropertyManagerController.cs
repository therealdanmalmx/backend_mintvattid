using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.PropertyManager;
using backend.models;
using backend.Services.PropertyManagerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PropertyManagerController : Controller
    {
        private readonly ILogger<PropertyManagerController> _logger;
        public readonly IPropertyManagerService _propertyManagerService;
        public PropertyManagerController(ILogger<PropertyManagerController> logger, IPropertyManagerService propertyManagerService)
        {
            _logger = logger;
            _propertyManagerService = propertyManagerService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetPropertyManagerDto>>>> Get()
        {

            return Ok(await _propertyManagerService.GetAllPropertyManager());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetPropertyManagerDto>>> GetPropertyManagerById(Guid id)
        {
            return Ok(await _propertyManagerService.GetPropertyManagerById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetPropertyManagerDto>>>> AddPropertyManager(AddPropertyManagerDto newPropertyManager)
        {
            return Ok(await _propertyManagerService.AddPropertyManager(newPropertyManager));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetPropertyManagerDto>>>> UpdatePropertyManager(UpdatePropertyManagerDto updatedPropertyManager)
        {
            var serviceResponse = await _propertyManagerService.UpdatePropertyManager(updatedPropertyManager);

            if (serviceResponse.Data == null)
            {
                return NotFound(serviceResponse);
            }
            return Ok(serviceResponse);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetPropertyManagerDto>>>> DeletePropertyManager(Guid id)
        {
            var serviceResponse = await _propertyManagerService.DeletePropertyManager(id);

            if (serviceResponse.Data == null)
            {
                return NotFound(serviceResponse);
            }
            return serviceResponse;
        }
    }
}