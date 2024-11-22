using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.WashRoom;
using backend.Services.WasRoomservices;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using backend.Services.RealEstateCompaniesServices;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WashroomController : ControllerBase
    {
        private readonly ILogger<WashroomController> _logger;
        private readonly IWashRoomServices _washRoomServices;

        public WashroomController(ILogger<WashroomController> logger, IWashRoomServices washRoomServices)
        {
            _washRoomServices = washRoomServices;
            _logger = logger;
        }

        [HttpGet("GetAllWashTimes")]
        public async Task<ActionResult<ServiceResponse<GetWasTimesPerWashRoomDto>>> GetWasTimesPerWashRoom(Guid washroomId)
        {
            return Ok(await _washRoomServices.GetWasTimesPerWashRoom(washroomId));
        }


        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetWashRoomsDto>>> AddWashroom(AddWashroomDto newWashroom)
        {
            return Ok(await _washRoomServices.AddWashroom(newWashroom));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetWashRoomsDto>>> UpdateWashroom(UpdateWashroomDto updatedWashroom, Guid id)
        {
            return Ok(await _washRoomServices.UpdateWashroom(updatedWashroom, id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetWashRoomsDto>>> DeleteWashroom(Guid id)
        {
            return Ok(await _washRoomServices.DeleteWashroom(id));
        }
    }
}