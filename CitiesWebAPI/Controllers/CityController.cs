using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CitiesWebAPI.Data;
using CitiesWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Remotion.Linq.Clauses;

namespace CitiesWebAPI.Controllers
{
    public class CityController : ControllerBase
    {
        private readonly CityDataContext _db;

        public CityController(CityDataContext db)
        {
            _db = db;

            if (!_db.Cities.Any())
            {
                _db.Cities.Add(new City { Name = "Odense", Description = "En by i Danmark", Places = new List<Place>{new Place{ Name = "H.C. Andersens hus", Description = "Et hus i Odense"}, new Place{Name = "Zoo", Description = "Et sted, hvor der bor nogle dyr"}}});
                _db.Cities.Add(new City { Name = "Aarhus", Description = "En anden by i Danmark", Places = new List<Place> { new Place { Name = "H.C. Andersens hus", Description = "Et hus i Odense" }, new Place { Name = "Zoo", Description = "Et sted, hvor der bor nogle dyr" } } });
                _db.Cities.Add(new City { Name = "København", Description = "Hovedstaden i Danmark", Places = new List<Place> { new Place { Name = "H.C. Andersens hus", Description = "Et hus i Odense" }, new Place { Name = "Zoo", Description = "Et sted, hvor der bor nogle dyr" } } });

                _db.SaveChanges();

            }
        }

        [Route("Cities")]
        public IActionResult GetCities(bool GetPointOfIntereset = false)
        {
            List<City> cities = _db.Cities.ToList();
            if (!GetPointOfIntereset)
            {
                return new ObjectResult(cities.Select(x => new { x.Id, x.Name, x.Description}));
            }

            return new ObjectResult(cities);

        }

        [Route("City/{id}")]
        public IActionResult GetCity(int id, bool GetPointOfInterest = false)
        {
            //if (!_db.cit)
            //{
                
            //}

            return new ObjectResult(_db.Cities.FirstOrDefault(x => x.Id == id));
        }

        
    }
}
