using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models.Account
{
	public class User:IdentityUser
	{
		[Required]
		public string Address { get; set; }

		
		public List<Order> Orders { get; set; }

		[NotMapped]
		public IList<string> RoleNames { get; set; }
	}
}
