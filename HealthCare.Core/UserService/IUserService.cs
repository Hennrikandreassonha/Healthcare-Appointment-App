using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using HealthCare.Core.Models.UserModels;


namespace HealthCare.Core.UserService
{
    public interface IUserService
    {
       User? AddUser(User user);
       User GetByEmail(string email);
        Task<string> GetEmailAsync();
        Task UpadetUserAsync(User user);

    }
}