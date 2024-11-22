using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace backend.Dtos.WashRoom
{
    public class UpdateWashroomDto
    {
        [JsonIgnore]
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;

    }
}