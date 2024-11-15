using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos.RealEstateCompany
{
    public class AddRealEstateCompanyDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Logo { get; set; }
    }
}