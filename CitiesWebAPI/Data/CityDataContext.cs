using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitiesWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CitiesWebAPI.Data
{
    public class CityDataContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Place> Places { get; set; }

        public CityDataContext(DbContextOptions<CityDataContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
            if (!Cities.Any()) { 
                Cities.Add(new City { Name = "Odense", Description = "En by i Danmark", Places = new List<Place> { new Place { Name = "H.C. Andersens hus", Description = "Et hus i Odense" }, new Place { Name = "Zoo", Description = "Et sted, hvor der bor nogle dyr" } } });
                Cities.Add(new City { Name = "Aarhus", Description = "En anden by i Danmark", Places = new List<Place> { new Place { Name = "Aros", Description = "Et museum med en regnbue" }, new Place { Name = "Den gamle by", Description = "En by der er gammel" } } });
                Cities.Add(new City { Name = "København", Description = "Hovedstaden i Danmark", Places = new List<Place> { new Place { Name = "Den lille havfrue", Description = "En dame med fiskehale og nogle gange uden hoved" }, new Place { Name = "Tivoli", Description = "En masse rutchebaner" } } });
                SaveChanges();
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
