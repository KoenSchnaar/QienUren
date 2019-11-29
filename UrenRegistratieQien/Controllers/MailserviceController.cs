using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using UrenRegistratieQien.Repositories;
using UrenRegistratieQien.Models;

namespace UrenRegistratieQien.Controllers
{
    public class MailserviceController : Controller
    {
        private readonly IDeclarationFormRepository declarationFormRepo;

        public MailserviceController(IDeclarationFormRepository DeclarationFormRepo)
        {
            declarationFormRepo = DeclarationFormRepo;
        }

        [HttpPost]
        public IActionResult MailService(DeclarationFormModel decModel, string uniqueId, string formId, string employeeName)
        {

            declarationFormRepo.EditDeclarationForm(decModel);
            declarationFormRepo.SubmitDeclarationForm(decModel);

            //message components
            string month = decModel.Month;

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Hans", "hanshanshans812@gmail.com"));
            message.To.Add(new MailboxAddress("Luuk", "luuk_wolferen@hotmail.com"));
            message.Subject = $"Urendeclaratieformulier van {employeeName} voor de maand {decModel.Month}";
            message.Body = new TextPart("plain")
            {
                Text = $"{employeeName} wil graag dat u het urendeclaratieformulier goedkeurt. Klik op de link om naar het formulier te gaan: https://localhost:5001/Mailservice/ApproveOrReject/?uniqueId=" + uniqueId + "&formId=" + formId
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("Smtp.gmail.com", 587, false);
                client.Authenticate("hanshanshans812@gmail.com", "Hans123!");
                client.Send(message);
                client.Disconnect(true);
            }
            return RedirectToRoute(new { controller = "Admin", action = "Admin" });
        }



        public IActionResult ApproveOrReject(string uniqueId, string formId)
        {

            var formIdAsInt = Convert.ToInt32(formId);

            if (declarationFormRepo.CheckIfIdMatches(uniqueId))
            {
                return View(declarationFormRepo.GetFormByFormId(formIdAsInt)); //pagina waar client kan regelen of het is goedgekeurd of niet
            } else
            {
                return View("~/Views/Mailservice/ErrorUnknownId.cshtml"); // pagina waar staat unknown id
            }
        }

        [HttpPost]
        public IActionResult Approve(string formId)
        {
            declarationFormRepo.ApproveForm(Convert.ToInt32(formId));

            return View(); //yay het is gelukt nu kun je deze pagina sluiten
        }

        [HttpPost]
        public IActionResult Reject(string formId, string comment)
        {

            declarationFormRepo.RejectForm(Convert.ToInt32(formId), comment);
            return View();
        }
    }
}
