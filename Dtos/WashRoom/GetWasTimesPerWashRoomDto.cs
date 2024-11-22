using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.WashTime;

namespace backend.Dtos.WashRoom
{
    public class GetWasTimesPerWashRoomDto
    {
        public string WashRoomName { get; set; } = string.Empty;
        public List<GetWashTimesDto>? Washtimes { get; set; }
    }
}