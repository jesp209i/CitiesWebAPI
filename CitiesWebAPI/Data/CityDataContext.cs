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
            
        }
    }
}
