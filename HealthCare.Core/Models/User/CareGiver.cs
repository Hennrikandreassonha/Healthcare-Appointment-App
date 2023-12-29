using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCare.Core.Models.User
{
    public class CareGiver : User
    {
        public CareGiverRoleEnum Role { get; set; }
        public bool IsAvailable { get; set; }
        public CareGiver()
        {
            
        }
    }
}