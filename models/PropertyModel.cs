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
        public RealEstateCompany RealEstateCompany { get; set; } = new RealEstateCompany { Name = string.Empty };
        public string PropertyName { get; set; } = string.Empty;
        public string PropertyStreet { get; set; } = string.Empty;
        public string PropertyCity { get; set; } = string.Empty;
        public string PropertyPostalCode { get; set; } = string.Empty;
        public int MyProperty { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
    }
}