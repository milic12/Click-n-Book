using Click_and_Book.Data;
using Click_and_Book.Models;
using Click_and_Book.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Click_and_Book.Controllers
{
    public class ApartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ApartmentsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var apartmentSearchModel = new ApartmentSearchModel();
            apartmentSearchModel.Categoryes = _context.ApartmentCategories.ToList();
            apartmentSearchModel.CityBlocks = _context.CityBlocks.ToList();
            apartmentSearchModel.FindApartments = new List<Apartment>();

            return View(apartmentSearchModel);
        }

        [HttpGet]
        public IActionResult Search(ApartmentSearchModel apartmentSearchModel)
        {
            var timeFrom = apartmentSearchModel.Reservation.TimeFrom;
            var timeTo = apartmentSearchModel.Reservation.TimeTo;
            apartmentSearchModel.Categoryes = _context.ApartmentCategories.ToList();
            apartmentSearchModel.CityBlocks = _context.CityBlocks.ToList();
            var reservations = _context.Reservations.Where(r => ((timeFrom > r.TimeFrom && timeFrom < r.TimeTo) ||
                                                                (timeTo > r.TimeFrom && timeTo < r.TimeTo)) &&
                                                                r.IsActive == true).ToList();
            var apartments = _context.Apartments.Where(a => a.OwnerId != _userManager.GetUserId(User)).ToList();
            foreach (var reserv in reservations)
            {
                var apartment = apartments.FirstOrDefault(a => a.Id == reserv.ApartmentId);
                apartments.Remove(apartment);
            }

            var apartmentsSort = apartments.Where(a => a.CityBlockId == apartmentSearchModel.Apartment.CityBlockId &&
                                                      a.MaxPeople == apartmentSearchModel.Apartment.MaxPeople &&
                                                      a.Balcony == apartmentSearchModel.Apartment.Balcony &&
                                                      a.AirConditioner == apartmentSearchModel.Apartment.AirConditioner)
                                          .ToList();

            apartmentsSort.AddRange(apartments.Where(a => a.MaxPeople >= apartmentSearchModel.Apartment.MaxPeople &&
                                                          a.CityBlockId == apartmentSearchModel.Apartment.CityBlockId &&
                                                          a.Balcony == apartmentSearchModel.Apartment.Balcony &&
                                                          a.AirConditioner == apartmentSearchModel.Apartment.AirConditioner)
                                              .OrderBy(a => a.MaxPeople)
                                              .ToList());

            apartmentsSort.AddRange(apartments.Where(a => a.MaxPeople >= apartmentSearchModel.Apartment.MaxPeople &&
                                                          a.CityBlockId == apartmentSearchModel.Apartment.CityBlockId &&
                                                          (a.Balcony == apartmentSearchModel.Apartment.Balcony ||
                                                          a.AirConditioner == apartmentSearchModel.Apartment.AirConditioner)&&
                                                          (a.AirConditioner == apartmentSearchModel.Apartment.AirConditioner !=
                                                         a.Balcony == apartmentSearchModel.Apartment.Balcony))
                                              .OrderBy(a => a.MaxPeople)
                                              .ToList());

            apartmentsSort.AddRange(apartments.Where(a => a.MaxPeople >= apartmentSearchModel.Apartment.MaxPeople &&
                                                          a.CityBlockId == apartmentSearchModel.Apartment.CityBlockId &&
                                                          a.Balcony != apartmentSearchModel.Apartment.Balcony &&
                                                          a.AirConditioner != apartmentSearchModel.Apartment.AirConditioner)
                                              .OrderBy(a => a.MaxPeople)
                                              .ToList());

            apartmentsSort.AddRange(apartments.Where(a => a.MaxPeople >= apartmentSearchModel.Apartment.MaxPeople &&
                                                          a.CityBlockId != apartmentSearchModel.Apartment.CityBlockId &&
                                                          a.Balcony == apartmentSearchModel.Apartment.Balcony &&
                                                          a.AirConditioner == apartmentSearchModel.Apartment.AirConditioner)
                                              .OrderBy(a => a.MaxPeople)
                                              .ToList());

            apartmentsSort.AddRange(apartments.Where(a => a.MaxPeople >= apartmentSearchModel.Apartment.MaxPeople &&
                                                         a.CityBlockId != apartmentSearchModel.Apartment.CityBlockId &&
                                                         (a.Balcony == apartmentSearchModel.Apartment.Balcony ||
                                                         a.AirConditioner == apartmentSearchModel.Apartment.AirConditioner)&&
                                                         (a.AirConditioner == apartmentSearchModel.Apartment.AirConditioner != 
                                                         a.Balcony == apartmentSearchModel.Apartment.Balcony))
                                             .OrderBy(a => a.MaxPeople)
                                             .ToList());

            apartmentsSort.AddRange(apartments.Where(a => a.MaxPeople >= apartmentSearchModel.Apartment.MaxPeople &&
                                                          a.CityBlockId != apartmentSearchModel.Apartment.CityBlockId &&
                                                          a.Balcony != apartmentSearchModel.Apartment.Balcony &&
                                                          a.AirConditioner != apartmentSearchModel.Apartment.AirConditioner)
                                              .OrderBy(a => a.MaxPeople)
                                              .ToList());


            apartmentSearchModel.FindApartments = apartmentsSort;
            foreach (var apartment in apartmentSearchModel.FindApartments)
            {
                apartment.Images = _context.Images.Where(i => i.ApartmentId == apartment.Id).ToList();
                apartment.Owner = _context.Owners.FirstOrDefault(u => u.Id == apartment.OwnerId);
                var days = (apartmentSearchModel.Reservation.TimeTo.Ticks - apartmentSearchModel.Reservation.TimeFrom.Ticks) / 863850233856;
                apartment.Price = (int)days * apartment.Price;
            }

            return View("Index", apartmentSearchModel);
        }
    }
}
