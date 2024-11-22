using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.User;
using backend.Dtos.UserRegister;
using backend.Services;
using backend.Services.AuthService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserServices _userServices;

        public UserController(ILogger<UserController> logger, IUserServices userServices)
        {
            _userServices = userServices;
            _logger = logger;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<Guid>>> UserRegister(UserRegisterDto request)
        {
            var response = await _userServices.RegisterUser(
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
        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<Guid>>> UserLogin(UserLoginDto request)
        {
            var response = await _userServices.LoginUser(request.Username, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}