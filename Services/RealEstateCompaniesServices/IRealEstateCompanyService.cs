using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.RealEstateCompany;

namespace backend.Services.RealEstateCompaniesServices
{
    public interface IRealEstateCompanyService
    {
        Task<ServiceResponse<List<GetRealEstateCompanyDto>>> GetAllRealEstateCompanies();
        Task<ServiceResponse<GetRealEstateCompanyDto>> GetRealEstateCompanyById(Guid id);
        Task<ServiceResponse<List<GetRealEstateCompanyDto>>> AddRealEstateCompany(AddRealEstateCompanyDto newRealEstateCompany);
        Task<ServiceResponse<GetRealEstateCompanyDto>> UpdateRealEstateCompany(UpdateRealEstateCompanyDto addRealEstateCompany);
        Task<ServiceResponse<List<GetRealEstateCompanyDto>>> DeleteRealEstateCompany(Guid id);
    }
}