using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCare.Core.Models.User;
using HealthCare.Core.UserService;
using HealthCare.Core.Data;

namespace HealthCare.Core.UserService
{
    public class UserService : IUserService
    {
        private readonly HealthcareContext _context;
        public UserService(HealthcareContext context)
        {
            _context = context;
        }
        public bool AddUser(User user)
        {
            try
            {
                if (user is Patient)
                    _context.Patient.Add((Patient)user);

                if (user is CareGiver)
                    _context.CareGiver.Add((CareGiver)user);


                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }
    }
}