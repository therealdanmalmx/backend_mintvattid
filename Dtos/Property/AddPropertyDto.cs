using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using backend.models;
using UserModel = backend.models.User;

namespace backend.Dtos.Property
{
    using System.Text.Json.Serialization;

    public class AddPropertyDto
    {
        public Guid RealEstateCompanyId { get; set; } = Guid.Empty;
        public string PropertyName { get; set; } = string.Empty;
        public string PropertyStreet { get; set; } = string.Empty;
        public string PropertyCity { get; set; } = string.Empty;
        public string PropertyPostalCode { get; set; } = string.Empty;

        [JsonIgnore]
        public UserModel? User { get; set; }
    }
}