using Click_and_Book.Data;
using Click_and_Book.Email;
using Click_and_Book.Models;
using Click_and_Book.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Click_and_Book.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _sender;

        public ReservationsController(UserManager<IdentityUser> userManager, 
                                      ApplicationDbContext context, 
                                      IConfiguration configuration,
                                      IEmailSender sender)
        {
            _userManager = userManager;
            _context = context;
            _configuration = configuration;
            _sender = sender;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var clientId = _userManager.GetUserId(User);
            var reservationsDb = _context.Reservations.Where(r => r.ClientId == clientId)
                                                      .OrderByDescending(r => r.IsActive).ThenBy(r => r.IsCancel).ToList();

            foreach (var reservation in reservationsDb)
            {
                reservation.Apartment = _context.Apartments.FirstOrDefault(a => a.Id == reservation.ApartmentId);
                reservation.Apartment.CityBlock = _context.CityBlocks.FirstOrDefault(b => b.Id == reservation.Apartment.CityBlockId);
                if(reservation.TimeTo < DateTime.Now)
                {
                    reservation.IsActive = false;
                }
            }

            _context.SaveChanges();

            var reservModel = new ReservationsModel
            {
                Reservations = reservationsDb
            };

            return View(reservModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reserve(Reservation reservation)
        {
            reservation.ClientId = _userManager.GetUserId(User);
            reservation.IsActive = true;
            reservation.IsCancel = false;
            var apartment = _context.Apartments.FirstOrDefault(a => a.Id == reservation.ApartmentId);
            var days = (reservation.TimeTo.Ticks - reservation.TimeFrom.Ticks) / 863850233856;
            reservation.Price = (int)days * apartment.Price;

            _context.Add(reservation);
            _context.SaveChanges();

            var owner = _context.Owners.FirstOrDefault(o => o.Id == apartment.OwnerId);
            var clientId = _userManager.GetUserId(User);
            var client = _context.Users.FirstOrDefault(c => c.Id == clientId);

            var emailDetails = new SendEmailDetails
            {
                FromName = _configuration["EmailName"],
                FromEmail = _configuration["Email"],
                ToEmail = client.Email,
                ToName = client.UserName,
                TemplateId = _configuration["TemplateIdInvoice"],
                TemplateData = new EmailTemplateData
                {
                    Number = reservation.Id,
                    Date=DateTime.Now.ToShortDateString(),
                    Owner = owner.UserName,
                    OwnerEmail=owner.Email,
                    ApartmentName=apartment.Name,
                    ReservationPrice=reservation.Price
                }
            };
            var result = await _sender.SendEmailAsync(emailDetails);

            if(result.Successful == false)
            {
                _context.Remove(reservation);
                _context.SaveChanges();

                return View("ReservationError", reservation);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Cancel(int Id)
        {
            var reservationDbo = _context.Reservations.FirstOrDefault(r => r.Id == Id);
            reservationDbo.IsActive = false;
            reservationDbo.IsCancel = true;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Approve(int Id)
        {
            var reservationDbo = _context.Reservations.FirstOrDefault(r => r.Id == Id);
            reservationDbo.IsApproved = true;

            _context.SaveChanges();

            return RedirectToAction("Index", "MyApartments");
        }
    }
}
