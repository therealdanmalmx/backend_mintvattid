using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.UserRegister;

namespace backend.models
{
    public class Property
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string PropertyName { get; set; } = string.Empty;
        public string PropertyStreet { get; set; } = string.Empty;
        public string PropertyCity { get; set; } = string.Empty;
        public string PropertyPostalCode { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("RealEstateCompanyId")]
        public Guid RealEstateCompanyId { get; set; }

        public User? User { get; set; }

    }

}