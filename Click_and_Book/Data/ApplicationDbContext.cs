using System;
using System.Collections.Generic;
using System.Text;
using Click_and_Book.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Click_and_Book.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<ApartmentCategory> ApartmentCategories { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<CityBlock> CityBlocks { get; set; }
        public DbSet<Image> Images { get; set; }

    }
}
