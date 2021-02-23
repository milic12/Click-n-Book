using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Click_and_Book.Models
{
    public class Image
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public string ImageName { get; set; }
    }
}
