using Click_and_Book.Data;
using Click_and_Book.Models;
using Click_and_Book.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Click_and_Book.Controllers
{
    public class MyApartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<IdentityUser> _userManager;

        public MyApartmentsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var  userId = _userManager.GetUserId(User);
            var apartments = _context.Apartments.Where(a => a.OwnerId == userId).ToList();
            var myApartmentsModel = new MyApartmantsModel
            {
                Apartments = new List<ApartmentRezModel>()
            };

            foreach (var apartmnet in apartments)
            {
                var apartmentRez = new ApartmentRezModel();
                apartmnet.Images = _context.Images.Where(i => i.ApartmentId == apartmnet.Id).ToList();
                apartmnet.Category = _context.ApartmentCategories.FirstOrDefault(c => c.Id == apartmnet.CategoryId);
                apartmnet.CityBlock = _context.CityBlocks.FirstOrDefault(b => b.Id == apartmnet.CityBlockId);
                apartmentRez.Apartment = apartmnet;
                apartmentRez.Reservations = _context.Reservations.Where(r => r.ApartmentId == apartmnet.Id && r.IsActive == true).ToList();
                myApartmentsModel.Apartments.Add(apartmentRez);         
            }

            return View(myApartmentsModel);
        }

        [HttpGet]
        public IActionResult New()
        {
            var categories = _context.ApartmentCategories.ToList();
            var cityBlocks = _context.CityBlocks.ToList();

            var apartment = new Apartment();
            apartment.Images = new List<Image>();

            var viewModel = new UpdateApartmentModel
            {
                Apartment = apartment,
                Categories = categories,
                CityBlocks = cityBlocks,
                Title = "New"
            };

            return View("MyApartmentForm", viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var categories = _context.ApartmentCategories.ToList();
            var cityBlocks = _context.CityBlocks.ToList();

            var apartment = _context.Apartments.FirstOrDefault(a => a.Id == Id);
            
            apartment.Images = _context.Images.Where(i => i.ApartmentId == Id).ToList();

            var viewModel = new UpdateApartmentModel
            {
                Apartment = apartment,
                Categories = categories,
                CityBlocks = cityBlocks,
                Title = "Edit"
            };

            return View("MyApartmentForm", viewModel);
        }

        public IActionResult DeleteImages(UpdateApartmentModel apartmentViewModel)
        {
            var categories = _context.ApartmentCategories.ToList();
            var cityBlocks = _context.CityBlocks.ToList();

            var apartment = _context.Apartments.FirstOrDefault(a => a.Id == apartmentViewModel.Apartment.Id);
            var imagesDbo = _context.Images.Where(i => i.ApartmentId == apartment.Id).ToList();

            foreach (var image in imagesDbo)
            {
                System.IO.File.Delete("./wwwroot/ApartmentImages/" + image.ImageName);
            }
            _context.RemoveRange(imagesDbo);

            _context.SaveChanges();

            return RedirectToAction("Edit", new { Id = apartment.Id});

        }

        public IActionResult Delete(int Id)
        {
            var apartmentDbo = _context.Apartments.FirstOrDefault(a => a.Id == Id);
            var imagesDbo = _context.Images.Where(i => i.ApartmentId == Id);
            var reservationsDbo = _context.Reservations.Where(r => r.ApartmentId == Id).ToList();

            foreach (var image in imagesDbo)
            {
                System.IO.File.Delete("./wwwroot/ApartmentImages/" + image.ImageName);
            }
            _context.RemoveRange(reservationsDbo);
            _context.RemoveRange(imagesDbo);
            _context.Remove(apartmentDbo);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAsync(UpdateApartmentModel apartmentViewModel)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }
            
            var apartment = apartmentViewModel.Apartment;

            if(apartment.Id != 0)
            {
                var apartmentDbo = _context.Apartments.FirstOrDefault(a => a.Id == apartment.Id);

                apartmentDbo.Name = apartment.Name;
                apartmentDbo.Price = apartment.Price;
                apartmentDbo.NumBeds = apartment.NumBeds;
                apartmentDbo.NumRooms = apartment.NumRooms;
                apartmentDbo.NumStars = apartment.NumStars;
                apartmentDbo.Balcony = apartment.Balcony;
                apartmentDbo.AirConditioner = apartment.AirConditioner;
                apartmentDbo.CategoryId = apartment.CategoryId;
                apartmentDbo.OwnerId = _userManager.GetUserId(User);
                apartmentDbo.Address = apartment.Address;
                apartmentDbo.CityBlockId = apartment.CityBlockId;
            }

            else
            {
                apartment.OwnerId = _userManager.GetUserId(User);

                await _context.AddAsync(apartment);
                await _context.SaveChangesAsync();
            }

            if (apartmentViewModel.ImageFiles != null)
            {
                var images = new List<Image>();
                foreach (var fileImage in apartmentViewModel.ImageFiles)
                {
                    var image = new Image();
                    image.ApartmentId = apartment.Id;
                    image.ImageName = UploadedFile(fileImage);
                    images.Add(image);
                }
                await _context.AddRangeAsync(images);
            }
            
            await _context.SaveChangesAsync();


            return RedirectToAction("Index");
        }

        private string UploadedFile(IFormFile formFile)
        {
            string uniqueFileName = null;

            if (formFile != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "ApartmentImages");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
