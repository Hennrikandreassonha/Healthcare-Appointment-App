using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Core.Models.UserModels;
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
        public string RegisterUser(RegisterDto userDto)
        {
            string successMsg = "You have been registerd as a ";
            // Checking if registerd user is a caregiver or a patient.
            User user = userDto.CaregiverCode.IsNullOrEmpty() ? new Patient() : new CareGiver();

            user.Email = userDto.Email;
            user.Role = userDto.CaregiverCode.IsNullOrEmpty() ? RoleEnum.Patient : RoleEnum.Doctor;
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            user.Gender = userDto.Gender;
            user.BirthDate = userDto.Birthdate;

            var registrationResult = _userService.AddUser(user);

            if (registrationResult != null && registrationResult is Patient)
            {
                return successMsg += "patient";
            }
             if (registrationResult != null && registrationResult is CareGiver)
            {
                return successMsg += "caregiver";
            }

            return "Error registering, please try again";
        }
    }
}