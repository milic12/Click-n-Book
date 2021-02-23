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
        [Required(ErrorMessage = "Please enter the Name")]
        public string Name { get; set; }
        [Display(Name = "Price per night")]
        [Range(0.01, 999999999, ErrorMessage = "Price must be greater than 0.00")]
        public decimal Price { get; set; }
        [Required]
        [Display(Name = "Number of beds")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter the number of beds")]
        public int NumBeds { get; set; }
        [Required]
        [Display(Name = "Number of rooms")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter the number of rooms")]
        public int NumRooms { get; set; }
        [Required]
        [Display(Name = "Number of people")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter the number of people")]
        public int MaxPeople { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "Please enter number of stars")]
        [Display(Name = "Stars")]
        public int NumStars { get; set; }
        [Required]
        public bool Balcony { get; set; }
        [Required]
        [Display(Name = "AirCondition")]
        public bool AirConditioner { get; set; }
        [Required]
        [Range(1, 8, ErrorMessage = "Please chose the category of the apartment")]
        [Display(Name = "Apartment category")]
        public int CategoryId { get; set; }
        public ApartmentCategory Category { get; set; }
        public string OwnerId { get; set; }
        public Owner Owner { get; set; }
        [Required]
        [StringLength(255, ErrorMessage ="Please enter the address")]
        public string Address { get; set; }
        [Required]
        [Range(1, 26, ErrorMessage = "Please chose the city block")]
        [Display(Name = "City block")]
        public int CityBlockId { get; set; }
        public CityBlock CityBlock { get; set; }
        public List<Image> Images { get; set; }
    }
}
