using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCare.Core.Models.User
{
    public class Patient : User
    {
        public GenderEnum Gender { get; set; }
        public DateTime BirthDate { get; set; }
    }
}