using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCare.Core.Models.Appointment
{
    public class Feedback
    {
        public int Id { get; set; }
        public string FeedbackText { get; set; }
        public DateTime FeedbackTime { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        //Koppla till appointment eller inte?
    }
}