using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace backend.Services.AuthService
{
    public class UserServices : IUserServices
    {
        private readonly DataContext _db;
        private readonly IConfiguration _configuration;

        public UserServices(ILogger<UserServices> logger, DataContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }
        public async Task<ServiceResponse<string>> LoginUser(string username, string password)
        {
            var serviceResponse = new ServiceResponse<string>();

            var user = await _db.Users.FirstOrDefaultAsync(user => user.UserName.ToLower().Equals(username.ToLower()));

            if (user == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Användare / lösenord kunde inte hittas";
            }

            else if (!VerifyUser(password, user.PasswordHash, user.PasswordSalt))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Användare / lösenord kunde inte hittas";
            }
            else
            {
                serviceResponse.Data = CreateToken(user);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Guid>> RegisterUser(User user, string password)
        {
            ServiceResponse<Guid> serviceResponse = new ServiceResponse<Guid>();

            if (await UserEmailExists(user.Email))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Email exists";
                return serviceResponse;
            }
            if (await UserExists(user.UserName))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Username exists";
                return serviceResponse;
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            serviceResponse.Data = user.Id;
            return serviceResponse;

        }

        public async Task<bool> UserExists(string username)
        {
            if (await _db.Users.AnyAsync(user => user.UserName.ToLower().Equals(username.ToLower())))
            {
                return true;
            }
            return false;
        }
        public async Task<bool> UserEmailExists(string email)
        {
            if (await _db.Users.AnyAsync(user => user.Email.ToLower().Equals(email.ToLower())))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyUser(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computeHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var tokenCode = _configuration.GetSection("AppSettings:Token").Value;
            if (string.IsNullOrEmpty(tokenCode))
            {
                throw new Exception("Token is not configured properly.");
            }
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenCode));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}