using System;
using System.ComponentModel.DataAnnotations;

namespace API_intro.Models
{
	public class Category : BaseEntity
	{
		[Required(ErrorMessage = "Cant be empty")]
		public string Name { get; set; }
	}
}

