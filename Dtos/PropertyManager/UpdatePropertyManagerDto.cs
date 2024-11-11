using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos.PropertyManager
{
    public class UpdatePropertyManagerDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "";
        public string StreetName { get; set; } = "";
        public string City { get; set; } = "";
        public string PostalCode { get; set; } = "";
        public string AdminName { get; set; } = "";
        public string AdminPhoneNumber { get; set; } = "";
        public string AdminEmail { get; set; } = "";
        // public List<Property> Properties { get; set; } = new List<Property>();
    }
}