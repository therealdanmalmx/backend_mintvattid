using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.WashTime;
using backend.Services.WashTimeService;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WashTimeController : ControllerBase
    {
        private readonly ILogger<WashTimeController> _logger;
        private readonly IWashTimeServices _washTimeServices;

        public WashTimeController(ILogger<WashTimeController> logger, IWashTimeServices washTimeServices)
        {
            _washTimeServices = washTimeServices;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetWashTimesDto>>>> AddWashTime(AddWasTimesDto newWashTime)
        {
            return Ok(await _washTimeServices.AddWashTime(newWashTime));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetWashTimesDto>>>> UpdateWashTime(UpdatedWashTimeDto updatedWashTime, Guid id)
        {
            return Ok(await _washTimeServices.UpdateWashTime(updatedWashTime, id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetWashTimesDto>>>> DeleteWastTime(Guid id)
        {
            return Ok(await _washTimeServices.DeleteWashTime(id));
        }

    }
}