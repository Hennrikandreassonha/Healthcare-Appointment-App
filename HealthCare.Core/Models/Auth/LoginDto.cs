using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCare.Core.Models.Auth
{
    [NotMapped]
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public LoginDto()
        {
            
        }
        public LoginDto(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}