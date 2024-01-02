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
        public bool AddInitialAppointment(int caregiverId, DateTime date)
        {
            //Adds the initial appointment with caregiverID and date.
            //When patient books this the patientID and service will be added aswell, making it a complete appointment.
            Appointment appointment = new(caregiverId, date);
            _context.Add(appointment);
            return true;
        }

        public void AddSampleData()
        {
            if (_context.CareGiver.Where(x => x.Email == "TestDoctor1").FirstOrDefault() != null)
                return;

            DateTime birthDate = new DateTime(1970, 1, 1);

            CareGiver careGiver1 = new("TestDoctor1", "Passhash", "Luke", "Skywalker", GenderEnum.Male, birthDate);
            CareGiver careGiver2 = new("TestDoctor2", "Passhash", "Legolas", "Elf", GenderEnum.Male, birthDate);

            _context.CareGiver.Add(careGiver1);
            _context.CareGiver.Add(careGiver2);
            _context.SaveChanges();

            var careGiverFromDb = _context.CareGiver.FirstOrDefault();
            Appointment appointment = new(careGiverFromDb.Id, DateTime.Now);
            Appointment appointment2 = new(careGiverFromDb.Id, DateTime.Now.AddDays(1));
            _context.Appointment.Add(appointment);
            _context.Appointment.Add(appointment2);
            _context.SaveChanges();
        }
    }
}

