using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.UserRegister;
using backend.Services;
using backend.Services.AuthService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("registerUser")]
        public async Task<ActionResult<ServiceResponse<Guid>>> RegisterUser(UserRegisterDto request)
        {
            var response = await _authRepository.RegisterUser(
                new User
                {
                    ApartmentNumber = request.ApartmentNumber,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    UserName = request.Email,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,

                }, request.Password
            );

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}