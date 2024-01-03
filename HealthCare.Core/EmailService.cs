using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using HealthCare.Core.Models.AppointmentModels;
using Humanizer;

namespace HealthCare.Core
{
    public class EmailService
    {
        //PW EMAIL: HealthCare1337123
        public string SenderEmail { get; set; }
        public string Password { get; set; }
        public string RecieverEmail { get; set; }
        public Appointment AppointmentDetails { get; set; }
        public EmailService()
        {

        }
        public EmailService(string recieverEmail, Appointment appointment)
        {
            //Förvara lösen och email på annat ställe
            SenderEmail = "healthcareappointmentapp@gmail.com";
            Password = "erhr ikni jexy odet";
            RecieverEmail = recieverEmail;
            AppointmentDetails = appointment;
        }
        public bool SendEmail()
        {
            MailMessage message = new MailMessage
            {
                From = new MailAddress(SenderEmail),
                Subject = $"Healthcare appointment booking confirmation"
            };

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(SenderEmail, Password),
                EnableSsl = true,
            };
            message.Body = BuildEmailContent();
            message.IsBodyHtml = true;
            message.To.Add(RecieverEmail);

            smtpClient.Send(message);

            return true;
        }
        public string BuildEmailContent()
        {
            string blueColor = "#1474bb";

            return $@"
                <html>
                    <body style='max-width: 600px; font-family: Arial, sans-serif;'>
                        <div style='border: 1px solid grey; padding: 0.5rem;'>
                            <h2>Appointment Confirmation</h2>
                            <p>Dear {AppointmentDetails.Patient.FirstName} {AppointmentDetails.Patient.LastName},</p>
                            <p>Your appointment has been successfully booked. Below are the details:</p>
                            <ul>
                                <li><strong>Doctor:</strong> {AppointmentDetails.CareGiver.FirstName} {AppointmentDetails.CareGiver.LastName}</li>
                                <li><strong>Service:</strong> General Checkup</li>
                                <li><strong>Date:</strong> {AppointmentDetails.DateTime.ToShortDateString()}</li>
                                <li><strong>Time:</strong> {AppointmentDetails.DateTime.Hour}:{AppointmentDetails.DateTime.Minute}</li>
                            </ul>
                            <p>Thank you for choosing our service. If you have any questions or need to make changes, please contact us.</p>
                            <p>Best regards,<br>The best healthcare app ever</p>
                        </div>
                    </body>
                </html>";
        }

    }
}