using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.models
{
    public class WashTime
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string? Name { get; set; } = string.Empty;
        public TimeSpan StartTime { get; set; } = TimeSpan.Zero;
        public TimeSpan EndTime { get; set; } = TimeSpan.Zero;
        public bool isBooked { get; set; } = false;

        public Guid WashRoomId { get; set; }
        public WashRoom? WashRoom { get; set; }
    }
}