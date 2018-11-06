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
