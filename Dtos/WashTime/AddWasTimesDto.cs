using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos.WashTime
{
    public class AddWasTimesDto
    {
        public string? Name { get; set; } = string.Empty;
        public TimeSpan StartTime { get; set; } = TimeSpan.Zero;
        public TimeSpan EndTime { get; set; } = TimeSpan.Zero;
        public Guid WashRoomId { get; set; }
    }
}