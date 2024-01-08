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
        public bool SaveFeedback(Appointment appointmentFeedback, string feedbackText)
        {
            if (appointmentFeedback.Rating >= 3)
            {
                _context.Update(appointmentFeedback);
                appointmentFeedback.Feedback = feedbackText;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Appointment> GetAllFeedBack()
        {
            //    List<Appointment> list = new ();

            //    list =


            return _context.Appointment.Where(x => x.Rating != null).ToList();
        }
    }
}



