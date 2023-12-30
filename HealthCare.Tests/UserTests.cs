﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.WebApp.Pages.Authentication;

namespace HealthCare.Tests
{
    public class UserTests
    {
        private readonly HealthcareContext _dbContext;
        private readonly IUserService _userService;
        private readonly AuthService _authService;

        public UserTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<HealthcareContext>().UseInMemoryDatabase("InMemory");
            _dbContext = new HealthcareContext(optionsBuilder.Options);
            _userService = new UserService(_dbContext);
            _authService = new AuthService(_userService);
        }
        [Fact]
        public void Register_User_Should_Return_Patient_Message()
        {
            //Arrange
            //Note that user has no code to be doctor.
            var userForTest = new RegisterDto("Test", "Password", "firstname", "lastname", GenderEnum.Male, DateTime.Now);

            //Act
            var result = _authService.RegisterUser(userForTest);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("You have been registerd as a patient", result);
        }
        [Fact]
        public void Register_User_Should_Return_Caregiver_Message()
        {
            //Arrange
            //Note that this user has a code to be a doctor.
            //Also note that we need to use a different email from first test.
            var userForTest = new RegisterDto("Test", "Password", "firstname", "lastname", GenderEnum.Male, DateTime.Now, "123");

            //Act
            var result = _authService.RegisterUser(userForTest);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("You have been registerd as a caregiver", result);
        }
        [Fact]
        public void Register_User_Email_Already_Exists_Return_Error_Message()
        {
            //Arrange
            //Adding user with same email 2 times. Resulting in error.
            var userForTest1 = new RegisterDto("Test", "Password", "firstname", "lastname", GenderEnum.Male, DateTime.Now, "123");
            var userForTest2 = new RegisterDto("Test", "Password", "firstname", "lastname", GenderEnum.Male, DateTime.Now, "123");

            //Act
            var result = _authService.RegisterUser(userForTest1);
            var result2 = _authService.RegisterUser(userForTest2);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("You have been registerd as a caregiver", result);

            //Error result since user already exists.
            Assert.NotNull(result2);
            Assert.Equal("Error registering, please try again", result2);
        }
        [Fact]
        public void Login_User_Should_Return_User()
        {
            //Arrange
            var userToRegister = new RegisterDto("Test", "Password", "firstname", "lastname", GenderEnum.Male, DateTime.Now, "123");

            //Act
            _authService.RegisterUser(userToRegister);
            var resultFromLogin = _authService.Authenticate(new LoginDto(userToRegister.Email, userToRegister.Password));

            //Assert
            Assert.NotNull(resultFromLogin);
            Assert.Equal(userToRegister.Email, resultFromLogin.Email);
        }
        [Fact]
        public void Login_User_Wrong_Password_Should_Return_Null()
        {
            //Arrange
            var userToRegister = new RegisterDto("Test", "Password", "firstname", "lastname", GenderEnum.Male, DateTime.Now, "123");

            //Act
            _authService.RegisterUser(userToRegister);
            var resultFromLogin = _authService.Authenticate(new LoginDto(userToRegister.Email, "WrongPass"));

            //Assert
            Assert.Null(resultFromLogin);
        }
    }
}
