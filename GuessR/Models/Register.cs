using GuessR.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GuessR.Models
{
	public class Register
	{
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Role { get; set; }


		//...
	}
}
