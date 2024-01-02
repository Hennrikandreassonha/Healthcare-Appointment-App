using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCare.Core.Models.User;
using HealthCare.Core.UserService;
using HealthCare.Core.Data;
using Microsoft.AspNetCore.Components.Authorization;

namespace HealthCare.Core.UserService
{
    public class UserService : IUserService
    {
        private readonly HealthcareContext _context;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public UserService(HealthcareContext context, AuthenticationStateProvider authenticationStateProvider)
        {
            _context = context;
            _authenticationStateProvider = authenticationStateProvider;
        }
        public bool AddUser(User user)
        {
            if (_context.Patient.Where(x => x.Email == user.Email).FirstOrDefault() != null || _context.CareGiver.Where(x => x.Email == user.Email).FirstOrDefault() != null)
                return false;

            try
            {
                if (user.Role == RoleEnum.Patient)
                    _context.Patient.Add((Patient)user);

                if (user is CareGiver careGiver && careGiver.Role == CareGiverRoleEnum.Doctor)
                {
                    _context.CareGiver.Add(careGiver);
                }

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }

        public User? GetByEmail(string email)
        {
            User user = new();
            user = _context.Patient.Where(x => x.Email == email).FirstOrDefault()!;
            if(user != null)
                return user;
                
            user = _context.CareGiver.Where(x => x.Email == email).FirstOrDefault()!;

            return user;
        }

        public async Task<string> GetEmailAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            return user.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value.ToString();
        }
    }
}