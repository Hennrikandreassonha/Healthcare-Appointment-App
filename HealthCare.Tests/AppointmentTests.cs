using HealthCare.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Core.Models.AppointmentModels;
using HealthCare.Core.Models.Appointment;
using HealthCare.Core.Migrations;

namespace HealthCare.Tests
{
    public class AppointmentTests
    {
        private readonly HealthcareContext _dbContext;

        public AppointmentTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<HealthcareContext>().UseInMemoryDatabase(databaseName: "AppointmentTestDB");
            _dbContext = new HealthcareContext(optionsBuilder.Options);
        }

        [Fact]
        public void GetAvailableTimes_ShouldReturn_CorrectTimes()
        {
            // Arrange
            var appointmentService = new AppointmentService(_dbContext);

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
            var appointmentService = new AppointmentService(_dbContext);
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
            var appointmentService = new AppointmentService(_dbContext);
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

    }

    // Arrange

    // Act

    // Assert
}
