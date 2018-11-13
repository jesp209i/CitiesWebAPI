using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CitiesWebAPI.Models
{
    public class Place
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
