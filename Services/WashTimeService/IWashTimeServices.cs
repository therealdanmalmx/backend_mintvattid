using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.WashTime;

namespace backend.Services.WashTimeService
{
    public interface IWashTimeServices
    {
        Task<ServiceResponse<List<GetWashTimesDto>>> GetWashTimes();
        Task<ServiceResponse<List<GetWashTimesDto>>> AddWashTime(AddWasTimesDto newWashTime);
        Task<ServiceResponse<GetWashTimesDto>> UpdateWashTime(UpdatedWashTimeDto updatedWashTime, Guid washTimeId);
        Task<ServiceResponse<List<GetWashTimesDto>>> DeleteWashTime(Guid id);
    }
}