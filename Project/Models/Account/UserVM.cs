using Microsoft.AspNetCore.Identity;

namespace Project.Models.Account
{
	public class UserVM
	{

		
			public IEnumerable<User> Users { get; set; }
			public IEnumerable<IdentityRole> Roles { get; set; }
		

	}
}
