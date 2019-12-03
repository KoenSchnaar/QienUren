using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using UrenRegistratieQien.DatabaseClasses;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using MimeKit;
using MailKit.Net.Smtp;

namespace UrenRegistratieQien.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(UserManager<Employee> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);

                //await _emailSender.SendEmailAsync(
                //    Input.Email,


                
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Hans", "hanshanshans812@gmail.com"));
                message.To.Add(new MailboxAddress("Qien", Input.Email));
                message.Subject = "Reset Wachtwoord";
                message.Body = new TextPart("html") { Text = $"Beste, <br> Om je wachtwoord te resetten: <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Klik hier!</a>. <br> Met vriendelijke groet" };
                   

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect("Smtp.gmail.com", 587, false);
                    client.Authenticate("hanshanshans812@gmail.com", "Hans123!"); //
                    client.Send(message);
                    client.Disconnect(true);


                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                //return Page();
            }
            return RedirectToPage("./ForgotPasswordConfirmation");
        }
    }
}
