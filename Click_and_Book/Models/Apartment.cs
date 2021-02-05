using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Click_and_Book.Models
{
    public class Apartment
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Price { get; set; }
        public int NumBeds { get; set; }
        public int NumRooms { get; set; }
        public int MaxPeople { get; set; }
        public int NumStars { get; set; }
        public int BuildYear { get; set; }
        public bool Balconiy { get; set; }
        public bool AirConditioner { get; set; }
        public int CategoryId { get; set; }
        public ApartmentCategory Category { get; set; }
        public string OwnerId { get; set; }
        public Owner Owner { get; set; }
    }
}
