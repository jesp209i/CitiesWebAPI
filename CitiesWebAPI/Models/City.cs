using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace CitiesWebAPI.Models
{
    public class City : ICloneable
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        public List<Place> Places { get; set; }

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
