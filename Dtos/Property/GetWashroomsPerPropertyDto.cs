using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.WashRoom;

namespace backend.Dtos.Property
{
    public class GetWashroomsPerPropertyDto
    {
        public string PropertyName { get; set; } = string.Empty;
        public List<GetWashRoomsDto>? WashRooms { get; set; }
    }
}