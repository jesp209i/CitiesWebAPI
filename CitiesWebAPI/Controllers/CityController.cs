using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using CitiesWebAPI.Data;
using CitiesWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Remotion.Linq.Clauses;
using Swashbuckle.AspNetCore.Swagger;

namespace CitiesWebAPI.Controllers
{
    //[Authorize]
    [Produces("application/json", "application/xml")]
    [Route("api/city")]
    public class CityController : ControllerBase
    {
        private readonly CityDataContext _db;
        private readonly IMapper _mapper;

        public CityController(CityDataContext db, IMapper map)
        {
            _db = db;
            _mapper = map;
        }

        [HttpGet]
        public IActionResult GetCities(bool getPointOfInterest = false)
        {
            //var cities = _db.Cities.Include("Places").Select( x => new FullCityDto { Id=x.Id, Name=x.Name, Description=x.Description, Places=x.Places }).ToList();
            var cities = _db.Cities.Include("Places").Select(x => _mapper.Map<FullCityDto>(x)).ToList();

            if (!getPointOfInterest)
            {
                //var simpleCities = cities.Select(x => new SimpleCityDto { Id = x.Id, Name = x.Name, Description = x.Description }).ToList();
                var simpleCities = cities.Select(x => _mapper.Map<SimpleCityDto>(x)).ToList();
                return Ok(simpleCities);
            }

            return Ok(cities);

        }
        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool getPointOfInterest = false)
        {
            var cityResult = _db.Cities.Include(x=> x.Places).FirstOrDefault(x => x.Id == id);
            if (cityResult == null)
            {
                return NotFound();
            }
            if (!getPointOfInterest)
            {
                return Ok(_mapper.Map<SimpleCityDto>(cityResult));
                //return new ObjectResult(_db.Cities.ToList().FindAll(x => x.Id == id).Select(x => new { x.Id, x.Name, x.Description }));
            }
            //return new ObjectResult(_db.Cities.Include(x => x.Places).FirstOrDefault(x => x.Id == id));
            return Ok(_mapper.Map<FullCityDto>(cityResult));

        }
        /// api/city
        [HttpPost]
        public IActionResult CreateCity([FromBody] SimpleCityDto newSimpleCity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //City newCity = new City { Name = newSimpleCity.Name, Description = newSimpleCity.Description };
            City newCity = _mapper.Map<City>(newSimpleCity);

            _db.Cities.Add(newCity);
            _db.SaveChanges();
            newSimpleCity.Id = newCity.Id;
            return CreatedAtAction("GetCity", new{id = newSimpleCity.Id}, newSimpleCity);
        }

        /// api/city/{cityid}
        [HttpDelete("{cityId:int}")]
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
        // api/city/{id}
        [HttpPut("{cityId:int}")]
        public IActionResult UpdateCity(int cityId, [FromBody]City city)
        {
            if (!ModelState.IsValid || cityId != city.Id )
            {
                return BadRequest(ModelState);
            }
            _db.Cities.Update(city);
            _db.SaveChanges();
            return Accepted();
        }

        [HttpPatch("{cityId}")]
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
