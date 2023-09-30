using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Models.Account;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Project.Data
{
	public class AppDbContext:IdentityDbContext<User>
	{
		
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}
		/*protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<item_order>().HasKey(am => new
			{
				am.ItemId,
				am.order_id
			});
			modelBuilder.Entity<item_order>().HasOne(m => m.order).WithMany(am => am.items_orders).HasForeignKey(m => m.ItemId);
			modelBuilder.Entity<item_order>().HasOne(m => m.item).WithMany(am => am.items_orders).HasForeignKey(m => m.order_id);
			base.OnModelCreating(modelBuilder);
		}*/
		
		public static async Task CreateAdminUser(IServiceProvider serviceProvider)
			{
				UserManager<User> userManager =
				serviceProvider.GetRequiredService<UserManager<User>>();
				RoleManager<IdentityRole> roleManager = serviceProvider
				.GetRequiredService<RoleManager<IdentityRole>>();
				string username = "admin";
				string password = "123456^Yy";
				string roleName = "Admin";
				// if role doesn't exist, create it
				if (await roleManager.FindByNameAsync(roleName) == null)
				{
					await roleManager.CreateAsync(new IdentityRole(roleName));
				}
				// if username doesn't exist, create it and add it to role
				if (await userManager.FindByNameAsync(username) == null)
				{
					User user = new User { UserName = username };
					var result = await userManager.CreateAsync(user, password);
					if (result.Succeeded)
					{
						await userManager.AddToRoleAsync(user, roleName);
					}
				}
			
		}
		public DbSet<Item> items { get; set; }
		public DbSet<Order> orders { get; set; }
		//public DbSet<item_order> items_orders { get; set; }
		public DbSet<User> users { get; set; }

	}
}
