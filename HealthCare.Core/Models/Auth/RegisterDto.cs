using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCare.Core.Models.Auth
{
    [NotMapped]
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderEnum Gender { get; set; }
        public DateTime Birthdate { get; set; } = DateTime.Now;
        public string? CaregiverCode { get; set; }

        public RegisterDto()
        {
        }
        public RegisterDto(string email, string password, string firstName, string lastName, GenderEnum gender, DateTime birthdate, string? caregiverCode = null)
        {
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Birthdate = birthdate;
            CaregiverCode = caregiverCode;
        }
    }
}