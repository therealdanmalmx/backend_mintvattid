using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using backend.Data;
using backend.Dtos.RealEstateCompany;
using backend.Services.RealEstateCompaniesServices;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RealEstateCompanyController : Controller
    {
        private readonly IRealEstateCompanyService _realEstateCompanyService;
        private readonly ILogger<RealEstateCompanyController> _logger;

        public RealEstateCompanyController(ILogger<RealEstateCompanyController> logger, IRealEstateCompanyService realEstateCompanyService)
        {
            _realEstateCompanyService = realEstateCompanyService;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetRealEstateCompanyDto>>>> GetAllRealEstateCompanies()
        {
            return Ok(await _realEstateCompanyService.GetAllRealEstateCompanies());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetRealEstateCompanyDto>>> GetRealEstateCompanyById(Guid id)
        {
            return Ok(await _realEstateCompanyService.GetRealEstateCompanyById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetRealEstateCompanyDto>>>> AddNewRealEstateCompany(AddRealEstateCompanyDto newRealEstateCompany)
        {
            return Ok(await _realEstateCompanyService.AddRealEstateCompany(newRealEstateCompany));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetRealEstateCompanyDto>>>> UpdateRealEstateCompany(UpdateRealEstateCompanyDto updateRealEstateCompany)
        {
            return Ok(await _realEstateCompanyService.UpdateRealEstateCompany(updateRealEstateCompany));
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<List<GetRealEstateCompanyDto>>>> DeleteRealEstateCompany(Guid id)
        {
            return Ok(await _realEstateCompanyService.DeleteRealEstateCompany(id));
        }
    }
}