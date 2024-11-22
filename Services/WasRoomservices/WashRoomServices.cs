using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using backend.Data;
using backend.Dtos.WashRoom;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.WasRoomservices
{
    public class WashRoomServices : IWashRoomServices
    {
        private readonly IMapper _mapper;
        private readonly DataContext _db;

        public WashRoomServices(IMapper mapper, DataContext db)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetWashRoomsDto>>> AddWashroom(AddWashroomDto newWashroom)
        {
            var serviceResponse = new ServiceResponse<List<GetWashRoomsDto>>();

            try
            {
                var washroom = _mapper.Map<WashRoom>(newWashroom);
                _db.Washrooms.Add(washroom);
                await _db.SaveChangesAsync();

                serviceResponse.Data = await _db.Washrooms
                    .Select(w => _mapper.Map<GetWashRoomsDto>(w))
                    .ToListAsync();

                serviceResponse.Message = "Tvättrum tillagt!";

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetWashRoomsDto>>> DeleteWashroom(Guid id)
        {
            var serviceResponse = new ServiceResponse<List<GetWashRoomsDto>>();
            try
            {
                var dbWashroom = await _db.Washrooms.FirstAsync(w => w.Id == id);
                _db.Washrooms.Remove(dbWashroom);
                await _db.SaveChangesAsync();

                serviceResponse.Data = await _db.Washrooms.Select(w => _mapper.Map<GetWashRoomsDto>(w)).ToListAsync();

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;

        }

        public async Task<ServiceResponse<List<GetWashRoomsDto>>> GetAllWashRooms()
        {
            var serviceResponse = new ServiceResponse<List<GetWashRoomsDto>>();

            try
            {
                var dbWashrooms = await _db.Washrooms.AsNoTracking().ToListAsync();

                if (dbWashrooms.Count == 0)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Det finns inga tvättrum registrerade";
                }

                serviceResponse.Data = dbWashrooms.Select(w => _mapper.Map<GetWashRoomsDto>(w)).ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetWashRoomsDto>> UpdateWashroom(UpdateWashroomDto updatedWashroom, Guid washroomId)
        {
            var serviceResponse = new ServiceResponse<GetWashRoomsDto>();
            WashRoom washroom = await _db.Washrooms.FirstOrDefaultAsync(w => w.Id.Equals(washroomId));

            try
            {
                if (washroom != null)
                {
                    if (!string.IsNullOrEmpty(updatedWashroom.Name))
                    {
                        washroom.Name = updatedWashroom.Name;
                    }

                    await _db.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetWashRoomsDto>(washroom);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Tvättrum hittades inte";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}