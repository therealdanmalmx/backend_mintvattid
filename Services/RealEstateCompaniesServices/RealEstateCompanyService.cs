using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using backend.Data;
using backend.Dtos.RealEstateCompany;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.RealEstateCompaniesServices
{
    public class RealEstateCompanyService : IRealEstateCompanyService
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;

        public RealEstateCompanyService(IMapper mapper, DataContext db)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetRealEstateCompanyDto>>> AddRealEstateCompany(AddRealEstateCompanyDto newRealEstateCompany)
        {
            var serviceResponse = new ServiceResponse<List<GetRealEstateCompanyDto>>();
            _db.RealEstateCompanies.Add(_mapper.Map<RealEstateCompany>(newRealEstateCompany));
            await _db.SaveChangesAsync();
            serviceResponse.Data = await _db.RealEstateCompanies.Select(realEstateCompany => _mapper.Map<GetRealEstateCompanyDto>(realEstateCompany)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetRealEstateCompanyDto>>> DeleteRealEstateCompany(Guid id)
        {
            var serviceResponse = new ServiceResponse<List<GetRealEstateCompanyDto>>();
            try
            {
                RealEstateCompany realEstateCompany = await _db.RealEstateCompanies.FirstAsync(realEstateCompany => realEstateCompany.Id == id);
                _db.Remove(realEstateCompany);
                await _db.SaveChangesAsync();

                serviceResponse.Data = _db.RealEstateCompanies.Select(realEstateCompany => _mapper.Map<GetRealEstateCompanyDto>(realEstateCompany)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetRealEstateCompanyDto>>> GetAllRealEstateCompanies()
        {
            var serviceResponse = new ServiceResponse<List<GetRealEstateCompanyDto>>();

            var dbRealEstateCompanies = await _db.RealEstateCompanies.ToListAsync();

            serviceResponse.Data = dbRealEstateCompanies.Select(realEstateCompany => _mapper.Map<GetRealEstateCompanyDto>(realEstateCompany)).ToList();

            return serviceResponse;

        }

        public async Task<ServiceResponse<GetRealEstateCompanyDto>> GetRealEstateCompanyById(Guid id)
        {
            var serviceResponse = new ServiceResponse<GetRealEstateCompanyDto>();
            var dbRealEstateCompanies = await _db.RealEstateCompanies.FirstOrDefaultAsync(realEstateCompany => realEstateCompany.Id.Equals(id));
            serviceResponse.Data = _mapper.Map<GetRealEstateCompanyDto>(dbRealEstateCompanies);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetRealEstateCompanyDto>> UpdateRealEstateCompany(UpdateRealEstateCompanyDto updateRealEstateCompany)
        {
            var serviceResponse = new ServiceResponse<GetRealEstateCompanyDto>();

            RealEstateCompany realEstateCompany = await _db.RealEstateCompanies.FirstOrDefaultAsync(realEstateCompany => realEstateCompany.Id.Equals(updateRealEstateCompany.Id));

            try
            {
                if (realEstateCompany != null)
                {
                    if (!string.IsNullOrEmpty(updateRealEstateCompany.Name))
                    {
                        realEstateCompany.Name = updateRealEstateCompany.Name;
                    }
                    if (!string.IsNullOrEmpty(updateRealEstateCompany.Logo))
                    {
                        realEstateCompany.Logo = updateRealEstateCompany.Logo;
                    }
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Real Estate Company not found";
                }

                await _db.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetRealEstateCompanyDto>(realEstateCompany);

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}