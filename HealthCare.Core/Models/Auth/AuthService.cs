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
            string caregiverCode = "Steve123";
            // Compares the users input with the caregiverCode above.
            if (userDto.CaregiverCode != caregiverCode && !userDto.CaregiverCode.IsNullOrEmpty())
            {
                return "ERROR: Invalid Caregiver Code";
            }   

            string successMsg = "You have been registered as a ";
            // Checking if registered user is a caregiver or a patient.
            User user = userDto.CaregiverCode == caregiverCode ? new CareGiver() : new Patient();

            user.Email = userDto.Email;
            user.Role = userDto.CaregiverCode == caregiverCode ? RoleEnum.Doctor : RoleEnum.Patient;
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
        public User? Authenticate(LoginDto loginDto)
        {
            var userAccount = _userService.GetByEmail(loginDto.Email);

            if (userAccount == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, userAccount.PasswordHash))
            {
                return null;
            }

            return userAccount;
        }
    }
}