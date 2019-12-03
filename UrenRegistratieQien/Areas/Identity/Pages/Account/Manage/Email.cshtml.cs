using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using UrenRegistratieQien.DatabaseClasses;
using MimeKit;
using MailKit.Net.Smtp;
using UrenRegistratieQien.Repositories;

namespace UrenRegistratieQien.Areas.Identity.Pages.Account.Manage
{
    public partial class EmailModel : PageModel
    {
        private readonly UserManager<Employee> _userManager;
        private readonly SignInManager<Employee> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IEmployeeRepository employeeRepo;

   
        public EmailModel(
            UserManager<Employee> userManager,
            SignInManager<Employee> signInManager,
            IEmailSender emailSender,
            IEmployeeRepository EmployeeRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            employeeRepo = EmployeeRepo;
        }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "New email")]
            public string NewEmail { get; set; }
        }

        private async Task LoadAsync(Employee user)
        {
            var email = await _userManager.GetEmailAsync(user);
            Email = email;

            Input = new InputModel
            {
                NewEmail = email,
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostChangeEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.NewEmail != email)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateChangeEmailTokenAsync(user, Input.NewEmail);
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmailChange",
                    pageHandler: null,
                    values: new { userId = userId, email = Input.NewEmail, code = code },
                    protocol: Request.Scheme);

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Qien", "hanshanshans812@gmail.com"));
                message.To.Add(new MailboxAddress(Input.NewEmail, Input.NewEmail));
                message.Subject = "Bevestiging Wijzigen E-mail Adres";
                message.Body = new TextPart("html")
                {
                    Text = $"Beste,<br>Je mail is gewijzigd naar {Input.NewEmail} <br> Met vriendelijke groet <br> Please confirm your account by < a href = '{HtmlEncoder.Default.Encode(callbackUrl)}' > clicking here </a>."
                };

                var message1 = new MimeMessage();
                message1.From.Add(new MailboxAddress("Qien", "hanshanshans812@gmail.com"));
                message1.To.Add(new MailboxAddress(Input.NewEmail, Input.NewEmail));
                message1.Subject = "Bevestiging Wijzigen E-mail Adres Voor Admin";
                message1.Body = new TextPart("html")
                {
                    Text = $"Beste,<br>... mail is gewijzigd van: {email} <br> naar: {Input.NewEmail} <br> Met vriendelijke groet"
                };

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect("Smtp.gmail.com", 587, false);
                    client.Authenticate("hanshanshans812@gmail.com", "Hans123!"); //
                    client.Send(message);
                    client.Send(message1);
                    client.Disconnect(true);

                    
                    StatusMessage = "Bevestigings link is naar uw e-mail verstuurd.";
                    employeeRepo.EditEmployeeMail(email, Input.NewEmail);
                    return RedirectToPage();
                }


            }
            StatusMessage = "Your email is unchanged.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = userId, code = code },
                protocol: Request.Scheme);

            //var message = new MimeMessage();
            //message.From.Add(new MailboxAddress("Hans", "hanshanshans812@gmail.com"));
            //message.To.Add(new MailboxAddress("Qien", Input.NewEmail));
            //message.Subject = "Bevestiging Wijzigen E-mail Adres";
            //message.Body = new TextPart("html")
            //{
            //    Text = $"MAIL 2 UIT DE CODE!,<br>Je mail is gewijzigd naar {Input.NewEmail} <br> Met vriendelijke groet" +
            //    $"Please confirm your account by < a href = '{HtmlEncoder.Default.Encode(callbackUrl)}' > clicking here </a>."
            //};


            //using (var client = new SmtpClient())
            //{
            //    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            //    client.Connect("Smtp.gmail.com", 587, false);
            //    client.Authenticate("hanshanshans812@gmail.com", "Hans123!"); //
            //    client.Send(message);
            //    client.Disconnect(true);
                StatusMessage = "Verification email sent. Please check your email.";
                return RedirectToPage();
            }
        }
    }



