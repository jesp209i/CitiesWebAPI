using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitiesWebAPI.Data;
using CitiesWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CitiesWebAPI.Controllers
{
    public class PlaceController : Controller
    {
       // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
