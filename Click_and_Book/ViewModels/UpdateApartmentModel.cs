using Click_and_Book.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Click_and_Book.ViewModels
{
    public class UpdateApartmentModel
    {
        public Apartment Apartment { get; set; }
        public IEnumerable<ApartmentCategory> Categories { get; set; }
        public IEnumerable<CityBlock> CityBlocks { get; set; }
        [Display(Name ="Apartment Images")]
        public List<IFormFile> ImageFiles { get; set; }
        public string Title { get; set; }
    }
}
