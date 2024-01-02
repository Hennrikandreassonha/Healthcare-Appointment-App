using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCare.Core.Models.User;
using Microsoft.AspNetCore.Components.Authorization;

namespace HealthCare.Core.UserService
{
    public interface IUserService
    {
       bool AddUser(User user);
       User GetByEmail(string email);
        Task<string> GetEmailAsync();

    }
}