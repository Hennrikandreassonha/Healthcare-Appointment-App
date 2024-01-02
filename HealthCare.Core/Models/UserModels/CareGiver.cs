using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCare.Core.Models.UserModels
{
    public class CareGiver : User
    {
        public bool IsAvailable { get; set; }
        public int AppointmentId { get; set; }
        public CareGiver()
        {
            
        }
    }
}