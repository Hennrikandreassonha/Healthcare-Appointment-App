using System;
using HealthCare.Core.Data;
using HealthCare.Core.Models.AppointmentModels;
namespace HealthCare.Core
{
    public class AppointmentService
    {
        private readonly HealthcareContext _context;
        public AppointmentService(HealthcareContext context)
        {
            _context = context;

        }
        public IEnumerable<Appointment> GetAppointmentsByDate(DateTime date)
        {
            return _context.Appointment
      .Where(x => x.DateTime.Year == date.Year && x.DateTime.DayOfYear == date.DayOfYear)
      .ToList();
        }

        public class AppointmentDetails
        {
            public string Id { get; set; }
            public string PatientId { get; set; }
            public string Details { get; set; }
        }
    }
}

