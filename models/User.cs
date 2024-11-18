using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ApartmentNumber { get; set; } = "";
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public UserRole Role { get; set; } = UserRole.User;

        [ForeignKey("PropertyId")]
        public Guid? PropertyId { get; set; }
    }
}