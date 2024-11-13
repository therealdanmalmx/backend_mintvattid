using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.models
{
    public class Property
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string PropertyName { get; set; } = string.Empty;
        public string PropertyStreet { get; set; } = string.Empty;
        public string PropertyCity { get; set; } = string.Empty;
        public string PropertyPostalCode { get; set; } = string.Empty;
        public string AdminName { get; set; } = string.Empty;
        public string AdminPhoneNumber { get; set; } = string.Empty;
        public string AdminEmail { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("PropertyManagerId")]
        public Guid PropertyManagerId { get; set; }
    }
}