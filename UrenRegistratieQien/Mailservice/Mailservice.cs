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
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Qien", "hanshanshans812@gmail.com")); //veranderen bij oplevering, hr@qien.nl
            message.To.Add(new MailboxAddress("Liza", "Lizavanderkruk@gmail.com")); //veranderen bij oplevering
            message.Subject = $"Uw inloggegevens wijzigen van uw Qien-medewerkers account";
            message.Body = new TextPart("plain")
            {
                Text = $"Beste {employee.FirstName} {employee.LastName}," + Environment.NewLine +
                $"" + Environment.NewLine +
                $"Welkom bij Qien. Er is een medewerkers account voor je aangemaakt met de volgende gegevens:" + Environment.NewLine +
                $"Inlognaam: {employee.Email}" + Environment.NewLine +
                $"Wachtwoord: {employee.FirstName}{employee.LastName}1!" + Environment.NewLine +
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

        public static void MailFormToClient(DeclarationFormModel decModel, string uniqueId, string formId, string employeeName)
        {
            //message components
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Hans", "hanshanshans812@gmail.com"));
            message.To.Add(new MailboxAddress("Liza", "lizavanderkruk@gmail.com"));
            message.Subject = $"Urendeclaratieformulier van {employeeName} voor de maand {decModel.Month}";
            var link = "https://localhost:5001/Mailservice/ApproveOrReject/?uniqueId=" + uniqueId + "&formId=" + formId;
            message.Body = new TextPart("html")

            {
                Text = $"{employeeName} wil graag dat je het urendeclaratieformulier goedkeurt. <br/> Klik op de link om naar het formulier te gaan: <a href={link}>Klik hier!</a>"
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

        public static void RejectMailToAdminAndEmployee(DeclarationFormModel decModel, string employeeName, string clientName)
        {
            //message components
            // mail naar Admin
            var message1 = new MimeMessage(); //naar admin
            message1.From.Add(new MailboxAddress("Qien", "hanshanshans812@gmail.com")); //aanpassen naar Admin mailadres
            message1.To.Add(new MailboxAddress("Liza", "lizavanderkruk@gmail.com")); //aanpassen naar Admin mailadres
            message1.Subject = $"Afgekeurd: Urendeclaratieformulier van {employeeName} voor de maand {decModel.Month}";
            message1.Body = new TextPart("html")
            {
                Text = $"Beste Qien,<br><br>De opdrachtgever heeft het urendeclaratieformulier van {employeeName} voor de maand {decModel.Month} afgekeurd. <br> Wil je deze afkeuring bekijken en het urendeclaratieformulier weer heropenen voor {employeeName}? <br><br> Met vriendelijke groet,<br><br> Team Qien"
            };

            //mail naar Employee
            var message2 = new MimeMessage(); //naar employee als urendeclaratieformulier is afgekeurd --> nog doen als het heropent is??
            message2.From.Add(new MailboxAddress("Qien", "hanshanshans812@gmail.com")); //aanpassen naar Admin mailadres
            message2.To.Add(new MailboxAddress("Liza", "lizavanderkruk@gmail.com")); //aanpassen naar Employee mailadres
            message2.Subject = $"Afgekeurd: Urendeclaratieformulier voor de maand {decModel.Month}";
            message2.Body = new TextPart("html")
            {
                Text = $"Beste {employeeName},<br><br>De opdrachtgever heeft jouw urendeclaratieformulier voor de maand {decModel.Month} afgekeurd. </br> Zodra deze weer beschikbaar is gesteld door Qien, kan je jouw urendeclaratieformulier opnieuw indienen.<br><br>Met vriendelijke groet,<br><br> Team Qien"
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("Smtp.gmail.com", 587, false);
                client.Authenticate("hanshanshans812@gmail.com", "Hans123!");
                client.Send(message1);
                client.Send(message2);
                client.Disconnect(true);
            }
        }
    }
}
