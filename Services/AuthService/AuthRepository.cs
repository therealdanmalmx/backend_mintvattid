using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.AuthService
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _db;

        public AuthRepository(DataContext db)
        {
            _db = db;
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
                serviceResponse.Data = user.Id.ToString();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Guid>> RegisterUser(User user, string password)
        {
            ServiceResponse<Guid> serviceResponse = new ServiceResponse<Guid>();

            if (await UserExists(user.UserName))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "En användare med det användarnamnet finns redan";
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
            if (await _db.Users.AnyAsync(user => user.UserName.ToLower() == username.ToLower()))
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
    }
}