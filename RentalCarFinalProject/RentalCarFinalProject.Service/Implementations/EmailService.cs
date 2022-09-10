using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Service.DTOs.AppUserDTOs;
using RentalCarFinalProject.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Implementations
{
    public class EmailService : IEmailService
    {
        public void ForgotPassword(AppUser user, string url, ForgotPasswordDTO forgotPassword)
        {
            throw new NotImplementedException();
        }

        public void Register(RegisterDTO registerDTO, string link)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("RentalCar", "testercodeacademy@gmail.com"));
            message.To.Add(new MailboxAddress(registerDTO.Name, registerDTO.Email));
            message.Subject = "Confirmation Email";

            string emailbody = "<a href='[URL]'>Confirmation Link</a>".Replace("[URL]", link);
            using var smtp = new SmtpClient();
            message.Body = new TextPart(TextFormat.Html) { Text = emailbody };
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("testercodeacademy@gmail.com", "qpgarphtmcmbxgwi");
            smtp.Send(message);
            smtp.Disconnect(true);
        }
    }
}
