using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using HealthCare.Core.Models.AppointmentModels;

namespace HealthCare.Core
{
    public class EmailService
    {
        //PW EMAIL: HealthCare1337123
        public string SenderEmail { get; set; }

        public string RecieverEmail { get; set; }
        public Appointment AppointmentDetails { get; set; }
        public EmailService()
        {
            // SenderEmail = ""
        }
    }
}