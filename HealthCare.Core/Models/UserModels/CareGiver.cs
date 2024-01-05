using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCare.Core.Models.UserModels
{
    public class CareGiver : User
    {

        // public bool? IsAvaiable { get; set; }

        [NotMapped]
        public TimeSpan[]? AvailableTimes { get; set; }

        public CareGiver()
        {
        }
        public CareGiver(string email, string passwordHash, string firstName, string lastName, GenderEnum gender, DateTime birthDate)
        {
            Email = email;
            PasswordHash = passwordHash;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            BirthDate = birthDate;
            Role = RoleEnum.Doctor; 
        }
    }
}