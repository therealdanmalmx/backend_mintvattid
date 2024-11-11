using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos.PropertyManager
{
    public class AddPropertyManagerDto
    {
        public string Name { get; set; } = "HSB";
        public string StreetName { get; set; } = "Södra Korsgatan 11";
        public string City { get; set; } = "Borås";
        public string PostalCode { get; set; } = "504 33";
        public string AdminName { get; set; } = "Lena Andersson";
        public string AdminPhoneNumber { get; set; } = "+46758956532";
        public string AdminEmail { get; set; } = "lena.anderssson@hsb.com";
        // public List<Property> Properties { get; set; } = new List<Property>();
    }
}