using System;
using System.ComponentModel.DataAnnotations;

namespace API_intro.Models
{
	public class City : BaseEntity
	{
        [Required]
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}

