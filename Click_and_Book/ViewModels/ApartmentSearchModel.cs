using Click_and_Book.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Click_and_Book.ViewModels
{
    public class ApartmentSearchModel
    {
        public Apartment Apartment { get; set; }
        public List<Apartment> FindApartments { get; set; }
        public IEnumerable<ApartmentCategory> Categoryes { get; set; }
        public IEnumerable<CityBlock> CityBlocks { get; set; }
        public Reservation Reservation { get; set; }

    }
}
