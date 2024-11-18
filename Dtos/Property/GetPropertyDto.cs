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
        public Guid RealEstateCompanyId { get; set; }
        public string PropertyName { get; set; } = string.Empty;
        public string PropertyStreet { get; set; } = string.Empty;
        public string PropertyCity { get; set; } = string.Empty;
        public string PropertyPostalCode { get; set; } = string.Empty;
        public List<backend.models.User>? Users { get; set; }
    }
}