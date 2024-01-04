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
        //1-5 stars
        public int Stars { get; set; }
        public DateTime FeedbackTime { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public Feedback(string feedbackText, int stars, int patientId)
        {
            FeedbackText = feedbackText;
            Stars = stars;
            PatientId = patientId;
            FeedbackTime = DateTime.Now;
        }
    }
    }