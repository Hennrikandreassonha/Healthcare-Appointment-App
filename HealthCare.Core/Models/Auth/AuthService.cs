using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Core.Models.User;
using HealthCare.Core.UserService;
using Microsoft.IdentityModel.Tokens;

namespace HealthCare.Core.Models.Auth
{
    public class AuthService
    {
        private readonly IUserService _userService;

        public AuthService(IUserService userService)
        {
            _userService = userService;
        }
        public bool RegisterUser(RegisterDto userDto)
        {
            Patient patient = new();
            CareGiver careGiver = new();
            bool registerSuccess;

            if (!userDto.CaregiverCode.IsNullOrEmpty())
            {
                careGiver.Email = userDto.Email;
                careGiver.Role = RoleEnum.Doctor;
                careGiver.FirstName = userDto.FirstName;
                careGiver.LastName = userDto.LastName;
                careGiver.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
                careGiver.Gender = userDto.Gender;
                careGiver.BirthDate = userDto.Birthdate;

                registerSuccess = _userService.AddUser(careGiver);
            }
            else
            {
                patient.Email = userDto.Email;
                patient.Role = RoleEnum.Patient;
                patient.FirstName = userDto.FirstName;
                patient.LastName = userDto.LastName;
                patient.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
                patient.Gender = userDto.Gender;
                patient.BirthDate = userDto.Birthdate;

                registerSuccess = _userService.AddUser(patient);
            }


            return registerSuccess;
        }
    }
}