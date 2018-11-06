using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CitiesWebAPI.Data;
using CitiesWebAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Clauses;

namespace CitiesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly CityDataContext _db;

        public CityController(CityDataContext db)
        {
            _db = db;
        }

        [Route("Cities")]
        public IActionResult GetCities(bool getPointOfInterest = false)
        {
            List<City> cities = _db.Cities.Include(x => x.Places).ToList();

            if (!getPointOfInterest)
            {
                return new ObjectResult(cities.Select(x => new { x.Id, x.Name, x.Description }));
            }

            return new ObjectResult(cities);

        }

        [Route("City/{id}")]
        public IActionResult GetCity(int id, bool getPointOfInterest = false)
        {

            if (!_db.Cities.ToList().Exists(x => x.Id == id))
            {
                return NotFound();
            }
            if (!getPointOfInterest)
            {
                return new ObjectResult(_db.Cities.ToList().FindAll(x => x.Id == id).Select(x => new { x.Id, x.Name, x.Description }));
            }
            return new ObjectResult(_db.Cities.Include(x => x.Places).FirstOrDefault(x => x.Id == id));

        }

        [HttpPost]
        [Route("CreateCity")]
        public IActionResult CreateCity([FromBody] City newCity)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Cities.Add(newCity);
            _db.SaveChanges();
            return CreatedAtAction("GetCity", new{id = newCity.Id}, newCity);

        }

        [HttpDelete]
        [Route("RemoveCity/{cityId}")]
        public IActionResult DeleteCity(int cityId)
        {

            City city = _db.Cities.FirstOrDefault(x => x.Id == cityId);

            if (city == null)
            {
                return BadRequest();
            }

            _db.Cities.Remove(city);
            _db.SaveChanges();
            return Accepted();
        }

        [HttpPut]
        [Route("UpdateCity/{cityId}")]
        public IActionResult UpdateCity([FromBody]City city, int cityId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Cities.Update(city);
            _db.SaveChanges();
            return Accepted();
        }

        [HttpPatch]
        [Route("UpdateCity/{cityId}")]
        public IActionResult UpdateCitySpecific([FromBody]JsonPatchDocument<City> patch, int cityId)
        {
            City city = _db.Cities.FirstOrDefault(x => x.Id == cityId);

            patch.ApplyTo(city, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = new
            {
                original = city,
                patched = patch
            };

            _db.Cities.Update(city);
            _db.SaveChanges();
            return Ok(model);
        }
        
    }
}
