using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Click_and_Book.CustomAttributes;

namespace Click_and_Book.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public Client Client { get; set; }
        public int ApartmentId { get; set; }
        public Apartment Apartment { get; set; }
        [TimeFromValidated]
        [FromNow]
        [Required(ErrorMessage = "Please enter check-in time")]
        public DateTime TimeFrom { get; set; }
        [FromNow]
        [Required(ErrorMessage = "Please enter check-out time")]
        public DateTime TimeTo { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public bool IsCancel { get; set; }
        public bool IsApproved { get; set; }
    }
}
