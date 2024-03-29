using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using HealthCare.Core.Models.Appointment;
using HealthCare.Core.Models.AppointmentModels;
using Humanizer;
using Microsoft.Extensions.Configuration;

namespace HealthCare.Core
{
    public class EmailService
    {
        //PW EMAIL: HealthCare1337123
        public string SenderEmail { get; set; }
        public string Password { get; set; }
        public string ReceiverEmail { get; set; }
        public Appointment AppointmentDetails { get; set; }
        public EmailService()
        {   
        }
        public EmailService(string receiverEmail, Appointment appointment, string email, string password)
        {
            //Förvara lösen och email på annat ställe
            SenderEmail = email;
            Password = password;
            ReceiverEmail = receiverEmail;
            AppointmentDetails = appointment;
        }
        public bool SendEmail(bool isConfirmation = true)
        {
            try
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
                message.Body = isConfirmation ? BuildEmailContentConfirmation() : BuildEmailContentCanceling();
                message.IsBodyHtml = true;
                message.To.Add(ReceiverEmail);
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            return true;
        }
        public bool SendCancelEmailByDoctor()
        {
            try
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
                message.Body = BuildEmailCancelledByCareGiver();
                message.IsBodyHtml = true;
                message.To.Add(ReceiverEmail);
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            return true;
        }
        public string BuildEmailContentConfirmation()
        {
            string serviceDisplayName = ServiceEnumExtensions.GetDisplayName(AppointmentDetails.Service ?? ServiceEnum.GeneralCheckup);

            return $@"
        <html>
            <body style='max-width: 600px; font-family: Arial, sans-serif;'>
                <div style='border: 1px solid grey; padding: 0.5rem;'>
                    <h2>Appointment Confirmation</h2>
                    <p>Dear {AppointmentDetails.Patient.FirstName} {AppointmentDetails.Patient.LastName},</p>
                    <p>Your appointment has been successfully booked. Below are the details:</p>
                    <ul>
                        <li><strong>Doctor:</strong> {AppointmentDetails.CareGiver.FirstName} {AppointmentDetails.CareGiver.LastName}</li>
                        <li><strong>Service:</strong> {serviceDisplayName} </li>
                        <li><strong>Date:</strong> {AppointmentDetails.DateTime.ToShortDateString()}</li>
                        <li><strong>Time:</strong> {AppointmentDetails.DateTime.Hour:00}:{AppointmentDetails.DateTime.Minute:00}</li>
                    </ul>
                    <div style='display:flex; align-items: center;'>
                        <div>
                            <p>Thank you for choosing our service. If you have any questions or need to make changes, please visit our website.</p>
                            <p>Best regards,<br><u>The best healthcare app ever</u></p>
                        </div>
                        <img src='https://i.ibb.co/BLBWZCV/logo.png' style='width: 140px; height: 100%; margin: 0 10px;'/>
                    </div>
                </div>
            </body>
        </html>";
        }
        public string BuildEmailContentCanceling()
        {
            string serviceDisplayName = ServiceEnumExtensions.GetDisplayName(AppointmentDetails.Service ?? ServiceEnum.GeneralCheckup);

            return $@"
                <html>
                    <body style='max-width: 600px; font-family: Arial, sans-serif;'>
                        <div style='border: 1px solid grey; padding: 0.5rem;'>
                            <h2>Appointment Cancelled</h2>
                            <p>Dear {AppointmentDetails.Patient.FirstName} {AppointmentDetails.Patient.LastName},</p>
                            <p>Your appointment has been successfully cancelled. Below are the details:</p>
                            <ul>
                                <li><strong>Doctor:</strong> {AppointmentDetails.CareGiver.FirstName} {AppointmentDetails.CareGiver.LastName}</li>
                                <li><strong>Service:</strong> {serviceDisplayName}</li>
                                <li><strong>Date:</strong> {AppointmentDetails.DateTime.ToShortDateString()}</li>
                                <li><strong>Time:</strong> {AppointmentDetails.DateTime.Hour:00}:{AppointmentDetails.DateTime.Minute:00}</li>
                            </ul>
                            
                            <div style='display:flex; align-items: center;'>
                                 <div>
                                     <p>Thank you for choosing our service. If you would like to book another appointment please visit our website.</p>
                                     <p>Best regards,<br><u>The best healthcare app ever</u></p>
                                 </div>
                                 <img src='https://i.ibb.co/BLBWZCV/logo.png' style='width: 140px; height: 100%; margin: 0 10px;'/>
                            </div>
                        </div>
                    </body>
                </html>";
        }
        public string BuildEmailCancelledByCareGiver()
        {
            string serviceDisplayName = ServiceEnumExtensions.GetDisplayName(AppointmentDetails.Service ?? ServiceEnum.GeneralCheckup);

            return $@"
                <html>
                    <body style='max-width: 600px; font-family: Arial, sans-serif;'>
                        <div style='border: 1px solid grey; padding: 0.5rem;'>
                            <h2>Appointment Cancelled</h2>
                            <p>Dear {AppointmentDetails.Patient.FirstName} {AppointmentDetails.Patient.LastName},</p>
                            <p>Your appointment has been cancelled by your doctor. Below are the details:</p>
                            <ul>
                                <li><strong>Doctor:</strong> {AppointmentDetails.CareGiver.FirstName} {AppointmentDetails.CareGiver.LastName}</li>
                                <li><strong>Service:</strong> {serviceDisplayName}</li>
                                <li><strong>Date:</strong> {AppointmentDetails.DateTime.ToShortDateString()}</li>
                                <li><strong>Time:</strong> {AppointmentDetails.DateTime.Hour:00}:{AppointmentDetails.DateTime.Minute:00}</li>
                            </ul>
                            
                            <div style='display:flex; align-items: center;'>
                                 <div>
                                     <p> If you would like to book another appointment please visit our website.</p>
                                     <p>Best regards,<br><u>The best healthcare app ever</u></p>
                                 </div>
                                 <img src='https://i.ibb.co/BLBWZCV/logo.png' style='width: 140px; height: 100%; margin: 0 10px;'/>
                            </div>
                        </div>
                    </body>
                </html>";
        }
    }
}