using Microsoft.Reporting.WebForms;
using office360.Models.EDMX;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;

namespace office360.Areas.HiringAndRecruitmentModule.HiringAndRecruitmentModuleHelper
{
    public class EmailHelper
    {
        private const string SmtpServer = "smtp.gmail.com";
        private const int SmtpPort = 587;
        private const string SenderEmail = "corporate@sehalsol.com";
        private const string SenderPassword = "Shazadadil@5966";
        public void SendEmail(string recipientEmail, string subject, string body)
        {
            try
            {
                MailMessage mailMessage = new MailMessage(SenderEmail, recipientEmail);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                SmtpClient smtpClient = new SmtpClient(SmtpServer, SmtpPort);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(SenderEmail, SenderPassword);
                smtpClient.Send(mailMessage);
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send email. Error: " + ex.Message);
            }
        }
        public void SendEmailWithAttachment(string recipientEmail, string subject, string body, byte[] attachmentData, string attachmentFileName, string attachmentMimeType)
        {
            using (MailMessage message = new MailMessage())
            {
                message.From = new MailAddress("corporate@sehalsol.com"); // Replace with your email address
                message.To.Add(new MailAddress(recipientEmail));
                message.Subject = subject;
                message.Body = body;

                // Attach the report file to the email
                using (MemoryStream stream = new MemoryStream(attachmentData))
                {
                    message.Attachments.Add(new Attachment(stream, attachmentFileName, attachmentMimeType));
                    using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")) // Replace with your SMTP server address
                    {
                        smtpClient.Port = 587; // Replace with the appropriate SMTP port number
                        smtpClient.Credentials = new NetworkCredential("corporate@sehalsol.com", "Shazadadil@5966"); // Replace with your SMTP credentials
                        smtpClient.EnableSsl = true; // Enable SSL if required
                        smtpClient.Send(message);
                    }
                }
            }
        }

    }


}