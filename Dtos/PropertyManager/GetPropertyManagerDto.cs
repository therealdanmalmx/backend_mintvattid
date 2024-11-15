using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using backend.models;

namespace backend.Dtos.PropertyManager
{
    public class GetPropertyManagerDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public backend.models.RealEstateCompany RealEstateCompanysName { get; set; } = new backend.models.RealEstateCompany { Name = string.Empty };
        public string? StreetName { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? PostalCode { get; set; } = string.Empty;
        public string? ContactName { get; set; } = string.Empty;
        public string? Contact { get; set; } = string.Empty;
        public string? ContactPhoneNumber { get; set; } = string.Empty;
        public string? ContactEmail { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}