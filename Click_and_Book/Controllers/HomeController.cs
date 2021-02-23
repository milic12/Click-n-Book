using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Click_and_Book.Models;
using Click_and_Book.Data;
using Microsoft.AspNetCore.Identity;
using Click_and_Book.Email;
using Microsoft.Extensions.Configuration;

namespace Click_and_Book.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;

        public HomeController(IEmailSender emailSender, IConfiguration configuration)
        {
            _emailSender = emailSender;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendEmailAsync(SendEmailDetails emailDetails)
        {
            emailDetails.ToEmail = _configuration["Email"];
            emailDetails.ToName = _configuration["EmailName"];
            emailDetails.FromEmail = _configuration["Email"];
            var result = await _emailSender.SendEmailAsync(emailDetails);
            if (result.Successful != true)
            {
                ModelState.AddModelError(string.Empty, "Email sender error!");
                return View("Contact", emailDetails);
            }

            return RedirectToAction("Contact");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
