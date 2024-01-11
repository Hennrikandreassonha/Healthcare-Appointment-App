using System;
using System.Security.Cryptography.X509Certificates;
using HealthCare.Core.Data;
using HealthCare.Core.Models.Appointment;
using HealthCare.Core.Models.AppointmentModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace HealthCare.Core
{
    public class AppointmentService
    {
        private readonly HealthcareContext _context;
        private readonly IConfiguration _config;
        public AppointmentService(HealthcareContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public IEnumerable<int> GetAvailableTimes()
        {
            return new List<int> { 8, 9, 10, 11, 13, 14, 15 };
        }
        public List<Appointment> GetCurUserAppointments(int userId)
        {
            return _context.Appointment.Where(x => x.Patient.Id == userId).ToList();
        }
        public IEnumerable<Appointment> GetAppointmentsByDate(DateTime date)
        {
            return _context.Appointment
                .Where(x => x.DateTime.Year == date.Year && x.DateTime.DayOfYear == date.DayOfYear && x.PatientId == null).Include(x => x.CareGiver)
                .ToList();
        }
        public bool AddInitialAppointmentsForADay(int caregiverId, DateTime date)
        {
            List<int> availableTimes = new();
            if (date.Day == DateTime.Now.Day)
            {
                availableTimes = GetAvailableTimes()
                .Where(x => x > DateTime.Now.Hour).ToList();
            }
            else
            {
                availableTimes = GetAvailableTimes().ToList();
            }

            List<Appointment> initialAppointments = new List<Appointment>();

            foreach (var hour in availableTimes)
            {
                var appointmentExists = _context.Appointment.Any(a =>
                    a.CareGiverId == caregiverId &&
                    a.DateTime.Year == date.Year &&
                    a.DateTime.Month == date.Month &&
                    a.DateTime.Day == date.Day &&
                    a.DateTime.Hour == hour);

                if (!appointmentExists)
                {
                    var appointment = new Appointment(caregiverId, new DateTime(date.Year, date.Month, date.Day, hour, 0, 0));
                    initialAppointments.Add(appointment);
                }
            }

            _context.AddRange(initialAppointments);
            _context.SaveChanges();

            return true;
        }
        public async Task<bool> AddInitialAppointment(int caregiverId, DateTime date, TimeSpan hour)
        {
            DateTime appointmentDateTime = date.Date + hour;

            var existingAppointment = await _context.Appointment.FirstOrDefaultAsync(a =>
                a.CareGiverId == caregiverId && a.DateTime == appointmentDateTime);

            if (existingAppointment == null)
            {
                var newAppointment = new Appointment
                {
                    CareGiverId = caregiverId,
                    DateTime = appointmentDateTime
                };

                _context.Appointment.Add(newAppointment);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
        public async Task<bool> CheckAppointmentExists(int doctorId, DateTime date, TimeSpan time)
        {
            DateTime appointmentDateTime = date.Date + time;

            var appointmentExists = await _context.Appointment.AnyAsync(a =>
                a.CareGiverId == doctorId &&
                a.DateTime == appointmentDateTime);

            return appointmentExists;
        }
        public async Task<bool> RemoveInitialAppointment(int caregiverId, DateTime date, TimeSpan hour)
        {
            DateTime appointmentDateTime = date.Date + hour;

            var appointmentToRemove = await _context.Appointment.FirstOrDefaultAsync(a =>
                a.CareGiverId == caregiverId && a.DateTime == appointmentDateTime);

            if (appointmentToRemove != null)
            {
                if (appointmentToRemove.Patient != null)
                {
                    var caregiver = await _context.CareGiver.FirstOrDefaultAsync(c => c.Id == caregiverId);

                    // Check if caregiver details exist and have an email
                    if (caregiver != null && !string.IsNullOrEmpty(caregiver.Email))
                    {
                        var email = _config.GetSection("Email:SenderEmail").Value;
                        var pass = _config.GetSection("Email:Password").Value;

                        var emailService = new EmailService(caregiver.Email, appointmentToRemove, email, pass);
                        emailService.SendEmail(false);
                    }

                    _context.Appointment.Remove(appointmentToRemove);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    _context.Appointment.Remove(appointmentToRemove);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }

            return false;
        }
        public IEnumerable<Appointment> GetInitialAppointmentsByDate(DateTime date)
        {
            return _context.Appointment.Where(x => x.DateTime.Year == date.Year && x.DateTime.DayOfYear == date.DayOfYear && x.PatientId == null)
                .Include(x => x.CareGiver).OrderBy(a => a.DateTime.Hour).ToList();
        }
        public bool RemoveInitialAppointmentsForADay(int caregiverId, DateTime date)
        {
            var appointmentsToRemove = _context.Appointment
                .Where(a => a.CareGiverId == caregiverId && a.DateTime.Date == date.Date && a.PatientId == null).ToList();

            _context.RemoveRange(appointmentsToRemove);
            _context.SaveChanges();

            return true;
        }
        public async Task<Appointment[]> GetAvailabilityByDate(int doctorId, DateTime date)
        {
            var availableAppointments = _context.Appointment
                .Where(x => x.DateTime.Year == date.Year && x.DateTime.DayOfYear == date.DayOfYear && x.CareGiverId == doctorId &&
                    (x.PatientId == null || x.PatientId != null))
                .Include(x => x.Patient)
                .ToList();

            var availableTimes = availableAppointments.Select(appointment => appointment.DateTime.Hour).Distinct().ToList();

            availableTimes.Sort();

            var sortedAppointments = availableAppointments
                .Where(appointment => availableTimes.Contains(appointment.DateTime.Hour))
                .OrderBy(appointment => appointment.DateTime.Hour)
                .ToList();

            return sortedAppointments.ToArray();
        }
        public async Task<bool> IsAppointmentForBooking(int doctorId, DateTime date)
        {
            //Checking if there is any appointment that can be toggled to be available for doctor. Using it to set buttons disabled or not.
            //Appointments that are available or booked.
            var appointments = _context.Appointment
                .Where(x => x.DateTime.Year == date.Year && x.DateTime.DayOfYear == date.DayOfYear && x.CareGiverId == doctorId &&
                    (x.PatientId == null || x.PatientId != null))
                .Include(x => x.Patient)
                .ToList();

            int[] timeSlots = { 8, 9, 10, 11, 13, 14, 15 };
            timeSlots = timeSlots.Where(x => x > DateTime.Now.Hour).ToArray();
            var availableTimes = appointments.Select(appointment => appointment.DateTime.Hour).Distinct().ToList();

            return timeSlots.Except(appointments.Select(x => x.DateTime.Hour).Where(x => x > DateTime.Now.Hour)).Any();
        }
        public bool AddBooking(Appointment appointment, int userId, ServiceEnum service)
        {
            //By adding the patient this will complete the booking.
            try
            {
                _context.Update(appointment);
                appointment.PatientId = userId;
                appointment.Service = service;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            return true;
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

