using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using UrenRegistratieQien.DatabaseClasses;
using UrenRegistratieQien.MailService;
using UrenRegistratieQien.Models;
using UrenRegistratieQien.Repositories;

namespace UrenRegistratieQien.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly IClientRepository clientRepo;
        private readonly SignInManager<Employee> _signInManager;
        private readonly UserManager<Employee> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        //private readonly IMailservice mailservice;

        public List<ClientModel> clients { get; set; }

        public RegisterModel(
            IClientRepository ClientRepo,
            UserManager<Employee> userManager,
            SignInManager<Employee> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
            //IMailservice mailservice)
        {
            
            clientRepo = ClientRepo;
            //clients = await clientRepo.GetAllClients();
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            //this.mailservice = mailservice;
        }
        
        
        

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [Range(0, int.MaxValue)]
            [Display(Name = "Client ID")]
            public int ClientId { get; set; }

            [Required (ErrorMessage ="Voornaam is verplicht")]
            [Display(Name = "Voornaam")]
            public string FirstName { get; set; }

            [Required (ErrorMessage = "Achternaam is verplicht")]
            [Display(Name = "Achternaam")]
            public string LastName { get; set; }

            [Required(ErrorMessage = "Straat/Huisnummer is verplicht")]
            [Display(Name = "Straatnaam + Huisnummer")]
            public string Adress { get; set; }

            [Required(ErrorMessage = "Postcode is verplicht")]
            [Display(Name = "Postcode")]
            public string ZIPCode { get; set; }

            [Required(ErrorMessage = "Woonplaats is verplicht")]
            [Display(Name = "Woonplaats")]
            public string Residence { get; set; }

            [Required(ErrorMessage = "Telefoonnummer is verplicht")]
            [Display(Name = "Telefoonnummer")]
            public string Phone { get; set; }

            [Required]
            [Display(Name = "Type gebruiker")]
            public int Role { get; set; }

            [Required(ErrorMessage = "Email is verplicht")]
            [EmailAddress(ErrorMessage = "Ongeldig e-mailadres")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            public  DateTime DateRegistered { get; set; }


            [Required(ErrorMessage = "Wachtwoord is verplicht")]
            [StringLength(24, ErrorMessage = "Het {0} moet minstens {2} en max {1} karakters lang zijn.", MinimumLength = 6)]
            [DataType(DataType.Password, ErrorMessage = "Ongeldig wachtwoord")]
            [Display(Name = "nieuwe wachtwoord")]
            public string Password { get; set; }

            [DataType(DataType.Password, ErrorMessage = "Ongeldig wachtwoord")]
            [Display(Name = "bevestig wachtwoord")]
            [Compare("Password", ErrorMessage = "Het wachtwoord en de bevestigings wachtwoord komen niet overeen.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new Employee {
                    UserName = Input.Email,
                    Email = Input.Email,
                    ClientId = Input.ClientId,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    Address = Input.Adress,
                    ZIPCode = Input.ZIPCode,
                    Residence = Input.Residence,
                    PhoneNumber = Input.Phone,
                    DateRegistered = DateTime.Now,
                    Role = Input.Role,
                    StartDateRole = DateTime.Now,
                    OutOfService = false,
                    EmailConfirmed = true
                };

                var userModel = new EmployeeModel
                {
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    Email = Input.Email,
                    ClientId = Input.ClientId,
                    Address = Input.Adress,
                    ZIPCode = Input.ZIPCode,
                    Residence = Input.Residence,
                    Phone = Input.Phone,
                    DateRegistered = DateTime.Now,
                    Role = Input.Role,
                    StartDateRole = DateTime.Now,
                    OutOfService = false
                };
                Mailservice.MailNewUser(userModel);
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
