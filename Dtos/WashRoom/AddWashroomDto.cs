using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PropertyModel = backend.models.Property;

namespace backend.Dtos.WashRoom
{
    public class AddWashroomDto
    {
        public string Name { get; set; } = string.Empty;

        public Guid? PropertyId { get; set; }
    }
}