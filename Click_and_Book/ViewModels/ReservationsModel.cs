using Click_and_Book.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Click_and_Book.ViewModels
{
    public class ReservationsModel
    {
        public IEnumerable<Reservation> Reservations { get; set; }
    }
}
