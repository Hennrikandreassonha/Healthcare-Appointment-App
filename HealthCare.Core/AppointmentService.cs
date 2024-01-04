﻿using System;
using HealthCare.Core.Data;
using HealthCare.Core.Models.Appointment;
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
        public bool AddInitialAppointment(int caregiverId, DateTime date)
        {
            //Adds the initial appointment with caregiverID and date.
            //When patient books this the patientID and service will be added aswell, making it a complete appointment.
            Appointment appointment = new(caregiverId, date);
            _context.Add(appointment);
            return true;
        }
        public bool AddBooking(Appointment appointment, int userId)
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
            for (int i = 0; i < 30; i++)
            {
                DateTime appointmentDateTime = DateTime.Today.AddHours(8).AddDays(i);
                Appointment appointment = new Appointment(careGiverFromDb.Id, appointmentDateTime);
                _context.Appointment.Add(appointment);
            }
            _context.SaveChanges();
        }

    }
}

