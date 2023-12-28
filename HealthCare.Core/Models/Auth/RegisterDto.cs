using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HealthCare.Core.Models.User;

namespace HealthCare.Core.Models.Auth
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderEnum Gender { get; set; }
        public DateTime Birthdate { get; set; }

        public RegisterDto()
        {
        }
        // public RegisterDto(Parameters)
        // {
            
        // }
    }
}