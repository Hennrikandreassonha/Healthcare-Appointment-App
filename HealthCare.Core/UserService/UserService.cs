using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCare.Core.Models.UserModels;
using HealthCare.Core.UserService;
using HealthCare.Core.Data;
using System.Reflection.Metadata.Ecma335;

namespace HealthCare.Core.UserService
{
    public class UserService : IUserService
    {
        private readonly HealthcareContext _context;
        public UserService(HealthcareContext context)
        {
            _context = context;
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
    }
}