using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCare.Core.Models.UserModels;
using HealthCare.Core.UserService;
using HealthCare.Core.Data;
using Microsoft.AspNetCore.Components.Authorization;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;


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
        public User? AddUser(User user)
        {
            if (_context.Patient.Where(x => x.Email == user.Email).FirstOrDefault() != null || _context.CareGiver.Where(x => x.Email == user.Email).FirstOrDefault() != null)
                return null;

            try
            {
                if (user.Role == RoleEnum.Patient)
                    _context.Patient.Add((Patient)user);

                if (user is CareGiver careGiver && careGiver.Role == RoleEnum.Doctor)
                {
                    _context.CareGiver.Add(careGiver);
                }

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

            return user.Role == RoleEnum.Patient ? (Patient)user : (CareGiver)user;
        }

        public User? GetByEmail(string email)
        {
            User user = new();
            user = _context.Patient.Where(x => x.Email == email).FirstOrDefault()!;
            if (user != null)
                return user;

            user = _context.CareGiver.Where(x => x.Email == email).FirstOrDefault()!;

            return user;
        }

        public List<CareGiver> GetCareGivers()
        {
            List<CareGiver> careGivers = new();
            careGivers = _context.CareGiver.ToList();
            return careGivers;
        }

        public async Task<string?> GetEmailAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            return user.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value.ToString();
        }

        public async Task UpdateUserAsync(User user)
        {
            User existingUser = new();
            if (user is Patient)
            {
                existingUser = await _context.Patient.FirstOrDefaultAsync(p => p.Id == user.Id);
            }
            else
            {
                existingUser = await _context.CareGiver.FirstOrDefaultAsync(c => c.Id == user.Id);
            }

            if (existingUser != null)
            {
                existingUser.Email = user.Email;
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("User not found in the database.");
            }
        }
    }
}