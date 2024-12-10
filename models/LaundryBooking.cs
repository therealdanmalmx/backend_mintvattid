using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.models
{
    public class LaundryBooking
    {
        public Guid Id { get; set; } = Guid.Empty;

        public Guid UserId { get; set; }

        public User? User { get; set; }
        public Guid WashingTimeId { get; set; }
        public WashTime? WashTime { get; set; }


    }
}