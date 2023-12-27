using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCare.Core.Models.User;

namespace HealthCare.Core.Models.Appointment
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public ServiceEnum Service { get; set; }
        public string CareGiverNotes { get; set; }
        public int PatientId { get; set; }
        public int CareGiverId { get; set; }
        public Patient Patient { get; set; }
        public CareGiver CareGiver { get; set; }
    }
}