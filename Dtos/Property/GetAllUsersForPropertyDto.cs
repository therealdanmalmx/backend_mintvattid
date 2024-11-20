using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserModel = backend.models.User;

namespace backend.Dtos.Property
{
    public class GetAllUsersForPropertyDto
    {
        public string PropertyName { get; set; } = string.Empty;
        public List<UserModel>? Users { get; set; }
    }
}
