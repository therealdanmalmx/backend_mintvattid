using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos.WashRoom
{
    public class GetWashRoomsDto
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;

        public Guid? PropertyId { get; set; }

    }
}