using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Click_and_Book.Email;
using Microsoft.Extensions.Configuration;

namespace Click_and_Book.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Email.IEmailSender _sender;
        private readonly IConfiguration _configuration;

        public RegisterConfirmationModel(UserManager<IdentityUser> userManager, Email.IEmailSender sender, IConfiguration configuration)
        {
            _userManager = userManager;
            _sender = sender;
            _configuration = configuration;
        }

        public string Email { get; set; }

        public bool DisplayConfirmAccountLink { get; set; }

        public string EmailConfirmationUrl { get; set; }

        public string ThisPageUrl { get; set; }

        public bool Error { get; set; }

        public async Task<IActionResult> OnGetAsync(string email, string returnUrl = null)
        {
            if (email == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"Unable to load user with email '{email}'.");
            }

            Email = email;


            // Once you add a real email sender, you should remove this code that lets you confirm the account
            //DisplayConfirmAccountLink = true;
            if (DisplayConfirmAccountLink)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                EmailConfirmationUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                    protocol: Request.Scheme);
            }

            else
            {
                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    EmailConfirmationUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);


                    var emailDetails = new SendEmailDetails
                    {
                        FromName = _configuration["EmailName"],
                        FromEmail = _configuration["Email"],
                        ToEmail = Email,
                        TemplateId = _configuration["TemplateIdVerify"],
                        TemplateData = new EmailTemplateData
                        {
                            ActionUrl = EmailConfirmationUrl
                        }
                    };

                    ThisPageUrl = $"/Identity/Account/RegisterConfirmation?email={Email}&returnUrl={returnUrl}";

                    var result = await _sender.SendEmailAsync(emailDetails);
                    Error = !(result.Successful);

            }

            return Page();
        }
    }
}
