using System;
using HealthCare.Core.Data;
using HealthCare.Core.Models.AppointmentModels;
using Microsoft.EntityFrameworkCore;
namespace HealthCare.Core
{
    public class AppointmentService
    {
        private readonly HealthcareContext _context;
        public AppointmentService(HealthcareContext context)
        {
            _context = context;
        }   
        public IEnumerable<int> GetAvailableTimes()
        {
            return new List<int> { 8, 9, 10, 11, 13, 14, 15 };
        }
        public bool AddInitialAppointment(int caregiverId, DateTime date, TimeSpan hour)
        {
            DateTime appointmentDateTime = date.Date + hour;

            var existingAppointment = _context.Appointment.FirstOrDefault(a => a.CareGiverId == caregiverId && a.DateTime == appointmentDateTime);

            if (existingAppointment == null)
            {
                Appointment appointment = new Appointment
                {
                    CareGiverId = caregiverId,
                    DateTime = appointmentDateTime,
                };

                _context.Appointment.Add(appointment);
                _context.SaveChanges();

                return true;
            }

            return false;
        }
        public bool AddInitialAppointmentsForADay(int caregiverId, DateTime date)
        {
            //Adds the initial appointment with caregiverID and date.
            //When patient books this the patientID and service will be added as well, making it a complete appointment.
            List<int> availableTimes = GetAvailableTimes().ToList();
            
            List<Appointment> initialAppointments = new List<Appointment>();

            foreach (var hour in availableTimes)
            {
                var initialAppointmentExists = _context.Appointment.Any(a =>
                    a.CareGiverId == caregiverId &&
                    a.DateTime.Year == date.Year &&
                    a.DateTime.Month == date.Month &&
                    a.DateTime.Day == date.Day &&
                    a.DateTime.Hour == hour);

                if (!initialAppointmentExists)
                {
                    var appointment = new Appointment(caregiverId, new DateTime(date.Year, date.Month, date.Day, hour, 0, 0));
                    initialAppointments.Add(appointment);
                }
            }

            _context.AddRange(initialAppointments);
            _context.SaveChanges();

            return true;
        }
        public IEnumerable<Appointment> GetInitialAppointmentsByDate(DateTime date)
        {
            List<int> availableTimes = GetAvailableTimes().ToList();

            var existingAppointments = _context.Appointment.Where(x => x.DateTime.Year == date.Year && x.DateTime.DayOfYear == date.DayOfYear 
                && x.PatientId == null || x.PatientId != null).Include(x => x.CareGiver).ToList();

            var missingAppointments = availableTimes.Where(time => !existingAppointments.Any(a => a.DateTime.Year == date.Year && a.DateTime.DayOfYear
                == date.DayOfYear && a.DateTime.Hour == time)).Select(time => new Appointment() { DateTime = new DateTime(date.Year, date.Month, date.Day
                , time, 0, 0) }).ToList();

            return existingAppointments.Concat(missingAppointments);
        }
        public bool RemoveInitialAppointment(int caregiverId, DateTime date, TimeSpan hour)
        {
            DateTime appointmentDateTime = date.Date + hour;

            var appointmentToRemove = _context.Appointment.Where(a => a.CareGiverId == caregiverId && a.DateTime == appointmentDateTime)
                .FirstOrDefault();

            if (appointmentToRemove != null)
            {
                _context.Appointment.Remove(appointmentToRemove);
                _context.SaveChanges();

                return true;
            }

            return false;
        }
        public bool RemoveInitialAppointmentsForADay(int caregiverId, DateTime date)
        {
            var appointmentsToRemove = _context.Appointment
                .Where(a => a.CareGiverId == caregiverId && a.DateTime.Date == date.Date && a.PatientId == null).ToList();

            _context.RemoveRange(appointmentsToRemove);
            _context.SaveChanges();

            return true;
        }        
        public bool AddBooking(Appointment appointment, int userId)
        {
            //By adding the patient this will complete the booking.
            try
            {
                _context.Update(appointment);
                appointment.PatientId = userId;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            return true;
        }
        public IEnumerable<Appointment> GetDoctorsAppointmentsByDate(int doctorId, DateTime date)
        {
            return _context.Appointment
                .Where(x => x.DateTime.Year == date.Year && x.DateTime.DayOfYear == date.DayOfYear && x.CareGiverId == doctorId)
                .Include(x => x.Patient).ToList();
        }
        public void AddSampleData()
        {
            if (_context.CareGiver.Where(x => x.Email == "TestDoctor1").FirstOrDefault() != null)
                return;

            DateTime birthDate = new DateTime(1970, 1, 1);

            CareGiver careGiver1 = new("TestDoctor1", "Passhash", "Luke", "Skywalker", GenderEnum.Male, birthDate);
            CareGiver careGiver2 = new("TestDoctor2", "Passhash", "Legolas", "Elf", GenderEnum.Male, birthDate);

            CareGiver giver = new();

            _context.CareGiver.Add(careGiver1);
            _context.CareGiver.Add(careGiver2);
            _context.SaveChanges();


            var careGiverFromDb = _context.CareGiver.FirstOrDefault();
            Appointment appointment = new(careGiverFromDb.Id, DateTime.Today.AddHours(8));
            Appointment appointment2 = new(careGiverFromDb.Id, DateTime.Today.AddDays(1).AddHours(12));
            _context.Appointment.Add(appointment);
            _context.Appointment.Add(appointment2);
            _context.SaveChanges();
        }
    }
}