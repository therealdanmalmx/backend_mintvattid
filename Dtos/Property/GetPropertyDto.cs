using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using backend.models;

namespace backend.Dtos.Property
{
    public class GetPropertyDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string PropertyName { get; set; } = "BRF Kråkan";
        public string PropertyStreet { get; set; } = "Lundagatan 20";
        public string PropertyCity { get; set; } = "Borås";
        public string PropertyPostalCode { get; set; } = "502 58";
        public string AdminName { get; set; } = "Nicklas Fredriksson";
        public string AdminPhoneNumber { get; set; } = "+467205695487";
        public string AdminEmail { get; set; } = "nickaf@gmail.com";

        [ForeignKey("PropertyManagerId")]
        public Guid PropertyManagerId { get; set; }
    }
}