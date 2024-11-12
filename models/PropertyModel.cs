using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.models
{
    public class Property
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public PropertyManager PropertyManager { get; set; } = new PropertyManager { Name = "HSB" };
        public string PropertyName { get; set; } = "BRF Kråkan";
        public string PropertyStreet { get; set; } = "Lundagatan 20";
        public string PropertyCity { get; set; } = "Borås";
        public string PropertyPostalCode { get; set; } = "502 58";
        public string AdminName { get; set; } = "Nicklas Fredriksson";
        public string AdminPhoneNumber { get; set; } = "+467205695487";
        public string AdminEmail { get; set; } = "nickaf@gmail.com";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}