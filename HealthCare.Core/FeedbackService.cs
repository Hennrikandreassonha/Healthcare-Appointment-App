using System;
using HealthCare.Core.Data;
using HealthCare.Core.Models.AppointmentModels;
namespace HealthCare.Core
{
    public class FeedbackService
    {
        private readonly HealthcareContext _context;

        public FeedbackService(HealthcareContext context)
        {
            _context = context;

        }
        public bool SaveFeedback(Appointment appointmentFeedback)
        {
            if (appointmentFeedback.Rating >= 3)
            {
                _context.Update(appointmentFeedback);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}



