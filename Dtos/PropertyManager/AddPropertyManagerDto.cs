using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace backend.Dtos.PropertyManager
{
    public class AddPropertyManagerDto
    {
        public RealEstateCompanies RealEstateCompaniesName { get; set; } = new RealEstateCompanies { Name = string.Empty };
        public string? StreetName { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? PostalCode { get; set; } = string.Empty;
        public string? ContactName { get; set; } = string.Empty;
        public string? Contact { get; set; } = string.Empty;
        public string? ContactPhoneNumber { get; set; } = string.Empty;
        public string? ContactEmail { get; set; } = string.Empty;

    }
}