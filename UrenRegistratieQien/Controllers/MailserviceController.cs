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


        public IActionResult ApproveOrReject(string uniqueId, string formId, bool commentNotValid)

        {

            var formIdAsInt = Convert.ToInt32(formId);

            if (declarationFormRepo.CheckIfIdMatches(uniqueId))
            {
                var declarationFormModel = declarationFormRepo.GetFormByFormId(formIdAsInt);
                return View(new RejectFormModel { declarationFormModel = declarationFormModel, commentNotValid = commentNotValid});
            } else
            {
                return View("~/Views/Mailservice/ErrorUnknownId.cshtml");
            }
        }

        [HttpPost]
        public IActionResult Approve(string formId)
        {
            declarationFormRepo.ApproveForm(Convert.ToInt32(formId));

            return View();
        }

        [HttpPost]
        public IActionResult Reject(int FormId, RejectFormModel rejectFormModel)
        {
            var declarationFormModel = declarationFormRepo.GetFormByFormId(FormId);
            var uniqueId = declarationFormModel.uniqueId;
            var comment = rejectFormModel.comment;
            var modelstate = ModelState.IsValid;
            if (ModelState.IsValid)
            {
                declarationFormRepo.RejectForm(FormId, comment);
                return View();
            } else
            {
                return RedirectToAction("ApproveOrReject", new { uniqueId = uniqueId, formId = FormId, commentNotValid = true});
            }
        }
    }
}
