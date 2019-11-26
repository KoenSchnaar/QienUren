using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;

namespace UrenRegistratieQien.Controllers
{
    public class MailserviceController : Controller
    {

        public IActionResult MailService()
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("Liza", "lizavanderkruk@gmail.com"));

            message.To.Add(new MailboxAddress("Koen", "koenschnaar@gmail.com"));

            message.Subject = "Feestje";
            message.Body = new TextPart("plain")
            {
                Text = "Ha Koen, dit mailtje is hopelijk aangekomen."
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("Smtp.gmail.com", 587, false);
                client.Authenticate("lizavanderkruk@gmail.com", "");
                client.Send(message);
                client.Disconnect(true);
            }
            return RedirectToRoute(new { controller = "Admin", action = "Admin" });
        }
    }
}
