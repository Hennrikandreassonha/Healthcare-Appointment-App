using HealthCare.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Core.Models.AppointmentModels;
using HealthCare.Core.Models.Appointment;
using HealthCare.Core.Migrations;
using Microsoft.Extensions.Configuration;

namespace HealthCare.Tests
{
    public class AppointmentTests
    {
        private readonly HealthcareContext _dbContext;
        private readonly IConfiguration _config;

        public AppointmentTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<HealthcareContext>().UseInMemoryDatabase(databaseName: "AppointmentTestDB");
            _dbContext = new HealthcareContext(optionsBuilder.Options);

            _config = new ConfigurationBuilder().Build();
        }

        [Fact]
        public void GetAvailableTimes_ShouldReturn_CorrectTimes()
        {
            // Arrange
            var appointmentService = new AppointmentService(_dbContext, _config);

            // Act
            var availableTimes = appointmentService.GetAvailableTimes();

            // Assert
            Assert.Equal(new List<int> { 8, 9, 10, 11, 13, 14, 15 }, availableTimes);
            _dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public void AddBooking_ShouldAdd_PatientToAppointment()
        {
            // Arrange
            var appointmentService = new AppointmentService(_dbContext, _config);
            CareGiver careGiver = new CareGiver { Id = 1, Email = "test@test.com", FirstName = "test", LastName = "testing", PasswordHash = "hash"};
            Appointment appointment = new Appointment { Id = 1, CareGiverId = careGiver.Id, DateTime = DateTime.Now };
            _dbContext.Add(careGiver);
            _dbContext.Add(appointment);
            _dbContext.SaveChanges();

            int userId = 123;
            ServiceEnum serviceType = ServiceEnum.GeneralCheckup;

            // Act
            var result = appointmentService.AddBooking(appointment, userId, serviceType);

            // Assert

            //Checks if booking was successful;
            Assert.True(result);

            var updatedAppointment = _dbContext.Appointment.FirstOrDefault(a => a.Id == appointment.Id);
            Assert.NotNull(updatedAppointment); // Ensure the appointment exists
            Assert.Equal(userId, updatedAppointment.PatientId); // Checks if patient ID was updated
            Assert.Equal(serviceType, updatedAppointment.Service); // Checks if service type was updated

            _dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public void GetAppointmentsByDate_ShouldReturn_CorrectAppointments()
        {
            // Arrange
            var appointmentService = new AppointmentService(_dbContext, _config);
            DateTime testDate = new DateTime(2024, 1, 8);
            CareGiver careGiver = new CareGiver { Id = 1, Email = "test@test.com", FirstName = "test", LastName = "testing", PasswordHash = "hash" };

            var appointments = new List<Appointment>
            {
                new Appointment { Id = 1, DateTime = testDate, PatientId = null, CareGiver = careGiver }, // This Appointment fulfills the criteria of the method and should be shown
                new Appointment { Id = 2, DateTime = testDate, PatientId = 2, CareGiver = careGiver }, // PatientId is not null here which means it has been booked and should not be shown
                new Appointment { Id = 3, DateTime = testDate.AddDays(1), PatientId = null, CareGiver = careGiver } // This Appointment is set to a different day and should not be shown
            };
            _dbContext.AddRange(appointments);
            _dbContext.SaveChanges();

            // Act
            var result = appointmentService.GetAppointmentsByDate(testDate);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result); // Checks that only the single correct appointment is being fetched

            _dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task AddInitialAppointment_Should_Add_Appointment()
        {
            // Arrange
            var appointmentService = new AppointmentService(_dbContext, _config);
            int caregiverId = 1;
            DateTime testDate = new DateTime(2024, 1, 8);
            TimeSpan testTime = new TimeSpan(8, 0, 0);

            // Act
            var result = await appointmentService.AddInitialAppointment(caregiverId, testDate, testTime);

            // Assert
            // Checks that the appointment was added
            Assert.True(result);

            // Checks that the added appointment show in the database
            var addedAppointment = await _dbContext.Appointment.FirstOrDefaultAsync(a =>
            a.CareGiverId == caregiverId && a.DateTime == testDate.Add(testTime));
            Assert.NotNull(addedAppointment);

            _dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public void AddInitialAppointmentsForADay_Should_Return_Appointments()
        {
            // Arrange
            var appointmentService = new AppointmentService(_dbContext, _config);
            int caregiverId = 1;
            DateTime testDate = new DateTime(2024, 1, 8);

            // Act
            var result = appointmentService.AddInitialAppointmentsForADay(caregiverId, testDate);

            // Assert
            // Checks that the appointments were added
            Assert.True(result);

            // Checks that the appointments were added to the database
            var appointments = _dbContext.Appointment
                    .Where(a => a.CareGiverId == caregiverId &&
                    a.DateTime.Year == testDate.Year &&
                    a.DateTime.Month == testDate.Month &&
                    a.DateTime.Day == testDate.Day)
                    .ToList();

            Assert.NotEmpty(appointments);

            _dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task RemoveInitialAppointment_Should_Remove_Appointment()
        {
            // Arrange
            var appointmentService = new AppointmentService(_dbContext, _config);
            int caregiverId = 1;
            DateTime testDate = new DateTime(2024, 1, 8);
            TimeSpan testTime = new TimeSpan(8, 0, 0);

            var appointments = new List<Appointment>
            {
                new Appointment { Id = 1, CareGiverId = caregiverId, DateTime = testDate.Add(testTime) },
                new Appointment { Id = 2, CareGiverId = caregiverId, DateTime = testDate.AddHours(2) }
            };

            _dbContext.AddRange(appointments);
            _dbContext.SaveChanges();

            // Act
            var result = await appointmentService.RemoveInitialAppointment(caregiverId, testDate, testTime);

            // Assert
            // Checks that the appointment was removed
            Assert.True(result);

            // Checks that the removed appointment doesn´t show in the database
            var removedAppointment = await _dbContext.Appointment.FirstOrDefaultAsync(a =>
                    a.CareGiverId == caregiverId && a.DateTime == testDate.Add(testTime));
            
            Assert.Null(removedAppointment);

            _dbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void RemoveInitialAppointmentsForADay_Should_Remove_Appointments()
        {
            // Arrange
            var appointmentService = new AppointmentService(_dbContext, _config);
            int caregiverId = 1;
            DateTime testDate = new DateTime(2024, 1, 8);

            // Act
            var result = appointmentService.RemoveInitialAppointmentsForADay(caregiverId, testDate);

            // Assert
            // Checks that the appointments were removed
            Assert.True(result);

            // Checks that the appointments were removed from the database
            var appointments = _dbContext.Appointment
                    .Where(a => a.CareGiverId == caregiverId &&
                    a.DateTime.Year == testDate.Year &&
                    a.DateTime.Month == testDate.Month &&
                    a.DateTime.Day == testDate.Day)
                    .ToList();

            Assert.Empty(appointments);

            _dbContext.Database.EnsureDeleted();
        }
    }

    // Arrange

    // Act

    // Assert
}