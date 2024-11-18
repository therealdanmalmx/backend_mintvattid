using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.AuthService
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<Guid>> RegisterUser(User user, string password);
        Task<ServiceResponse<string>> LoginUser(string username, string password);

        Task<bool> UserExists(string username);
    }
}