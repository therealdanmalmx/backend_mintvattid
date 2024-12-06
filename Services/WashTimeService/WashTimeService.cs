using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using backend.Data;
using backend.Dtos.WashTime;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.WashTimeService
{
    public class WashTimeServices : IWashTimeServices
    {
        private readonly IMapper _mapper;
        private readonly DataContext _db;
        public WashTimeServices(IMapper mapper, DataContext db)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetWashTimesDto>>> GetWashTimes()
        {
            var serviceResponse = new ServiceResponse<List<GetWashTimesDto>>();

            try
            {
                var dbWashTime = await _db.Washtimes.ToListAsync();

                if (dbWashTime.Count == 0)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Hittade inga tvättider";
                }
                serviceResponse.Data = dbWashTime.Select(w => _mapper.Map<GetWashTimesDto>(w)).ToList();


            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetWashTimesDto>>> AddWashTime(AddWasTimesDto newWashTime)
        {
            var serviceResponse = new ServiceResponse<List<GetWashTimesDto>>();

            try
            {
                _db.Washtimes.Add(_mapper.Map<WashTime>(newWashTime));
                await _db.SaveChangesAsync();

                serviceResponse.Data = await _db.Washtimes.Select(w => _mapper.Map<GetWashTimesDto>(w)).ToListAsync();

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetWashTimesDto>> UpdateWashTime(UpdatedWashTimeDto updatedWashTime, Guid washTimeId)
        {
            var serviceResponse = new ServiceResponse<GetWashTimesDto>();

            try
            {
                WashTime? washTime = await _db.Washtimes.FirstOrDefaultAsync(w => w.Id == washTimeId);

                if (washTime != null)
                {
                    if (!string.IsNullOrEmpty(updatedWashTime.Name))
                    {
                        washTime.Name = updatedWashTime.Name;
                    }
                    if (updatedWashTime.StartTime != TimeSpan.Zero)
                    {
                        washTime.StartTime = updatedWashTime.StartTime;
                    }
                    if (updatedWashTime.EndTime != TimeSpan.Zero)
                    {
                        washTime.EndTime = updatedWashTime.EndTime;
                    }
                    if (updatedWashTime.WashRoomId != Guid.Empty)
                    {
                        washTime.WashRoomId = updatedWashTime.WashRoomId;
                    }
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Hittade inga tvättider";
                }

                await _db.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetWashTimesDto>(washTime);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;

        }

        public async Task<ServiceResponse<List<GetWashTimesDto>>> DeleteWashTime(Guid id)
        {
            var serviceResponse = new ServiceResponse<List<GetWashTimesDto>>();

            try
            {
                var dbWashtime = await _db.Washtimes.FirstAsync(w => w.Id == id);
                _db.Washtimes.Remove(dbWashtime);
                await _db.SaveChangesAsync();


                serviceResponse.Data = await _db.Washtimes.Select(w => _mapper.Map<GetWashTimesDto>(w)).ToListAsync();

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