using Click_and_Book.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Click_and_Book.ViewModels
{
    public class MyApartmantsModel
    {
        public List<ApartmentRezModel> Apartments { get; set; }
    }

    public class ApartmentRezModel
    {
        public Apartment Apartment { get; set; }
        public IEnumerable<Reservation> Reservations { get; set; }
    }
}
