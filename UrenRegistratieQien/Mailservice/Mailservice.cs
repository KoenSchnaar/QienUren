using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrenRegistratieQien.Models;
using UrenRegistratieQien.Repositories;
using MailKit.Net.Smtp;
using static Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal.ExternalLoginModel;

namespace UrenRegistratieQien.MailService
{
    public class Mailservice
    {

        public static void MailNewUser(EmployeeModel employee)
        {
            //message components
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Hans", "hanshanshans812@gmail.com"));
            message.To.Add(new MailboxAddress("Koen", "Lizavanderkruk@gmail.com"));

            message.Subject = $"Uw inloggegevens wijzigen van uw Qien-medewerkers account";
            message.Body = new TextPart("plain")
            {
                Text = $"Beste {employee.FirstName} {employee.LastName}," + Environment.NewLine +
                $"" + Environment.NewLine +
                $"Welkom bij Qien. Er is een medewerkers account voor je aangemaakt met de volgende gegevens:" + Environment.NewLine +
                $"Inlognaam: {employee.Email}" + Environment.NewLine +
                $"Wachtwoord: {employee.FirstName}{employee.LastName}" + Environment.NewLine +
                $"" + Environment.NewLine +
                $"Log hier in om je wachtwoord te veranderen https://localhost:5001/Identity/Account/Manage/ChangePassword" + Environment.NewLine + //veranderen site 
                $"" + Environment.NewLine +
                $"Met vriendelijke groet," + Environment.NewLine +
                $"" + Environment.NewLine +
                $"Team Qien" 
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("Smtp.gmail.com", 587, false);
                client.Authenticate("hanshanshans812@gmail.com", "Hans123!");
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
