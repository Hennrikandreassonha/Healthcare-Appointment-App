using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCare.Core.Models.Appointment;

namespace HealthCare.Core.Models.AppointmentModels
{
    public class Appointment
        {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public ServiceEnum? Service { get; set; }
        public string? Feedback { get; set; }
        public int? Rating { get; set; }
        public int? PatientId { get; set; }
        public int CareGiverId { get; set; }
        public Patient Patient { get; set; }
        public CareGiver CareGiver { get; set; }
        public Appointment()
        {
            
        }
        
        public Appointment(int caregiverId, DateTime date)
        {
            DateTime = date;
            CareGiverId = caregiverId;
        }
    }
}