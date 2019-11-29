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
            message.To.Add(new MailboxAddress("Koen", "koenschnaar@gmail.com"));
            message.Subject = $"Uw inloggegevens wijzigen van uw Qien-medewerkers account";
            message.Body = new TextPart("plain")
            {
                Text = $"Beste {employee.FirstName} {employee.LastName}," +
                $"" +
                $"Welkom bij Qien. Er is een medewerkers account voor je aangemaakt met de volgende gegevens:" +
                $"Inlognaam: {employee.Email}" +
                $"Wachtwoord: {employee.FirstName}{employee.LastName}" +
                $"" +
                $"Log hier in om je wachtwoord te veranderen https://www.iets.nl" + //veranderen site
                $"" +
                $"Met vriendelijke groet," +
                $"" +
                $"Team Qien" 

                //$"{employeeName} wil graag dat u het urendeclaratieformulier goedkeurt. " +
                //$"Klik op de link om naar het formulier te gaan: " +
                //$"https://localhost:5001/Mailservice/ApproveOrReject/?uniqueId=" + uniqueId + 
                //"&formId=" + formId
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
