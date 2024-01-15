using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCare.Core.Models.UserModels
{
    public class Patient : User
    {
        public List<MedicalJournalEntry> JournalEntries { get; set; }

        public Patient()
        {

        }
        public Patient(string passwordHash, string email, string firstName, string lastName, GenderEnum gender, RoleEnum role, DateTime birthdate)
        {
            PasswordHash = passwordHash;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Role = role;
            BirthDate = birthdate;
        }
    }
}