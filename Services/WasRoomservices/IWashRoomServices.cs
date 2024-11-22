using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.WashRoom;

namespace backend.Services.WasRoomservices
{
    public interface IWashRoomServices
    {
        Task<ServiceResponse<List<GetWashRoomsDto>>> GetWashRooms();
        Task<ServiceResponse<List<GetWasTimesPerWashRoomDto>>> GetWasTimesPerWashRoom(Guid washroomId);
        Task<ServiceResponse<List<GetWashRoomsDto>>> AddWashroom(AddWashroomDto newWashroom);
        Task<ServiceResponse<GetWashRoomsDto>> UpdateWashroom(UpdateWashroomDto updatedWashroom, Guid washroomId);
        Task<ServiceResponse<List<GetWashRoomsDto>>> DeleteWashroom(Guid id);
    }
}