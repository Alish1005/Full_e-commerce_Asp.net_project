using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Data.Repository;
using Project.Models.Account;
using Project.Models;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddControllersWithViews();

		//Services configuration
		builder.Services.AddTransient<IEntityRepository<Item>, EntityRepository<Item>>();
		builder.Services.AddTransient<IEntityRepository<User>, EntityRepository<User>>();

		builder.Services.AddScoped(typeof(IRepoOrder), typeof(RepoOrder)); //When it is not Geniric

		builder.Services.AddIdentity<User, IdentityRole>(options =>
		{
			options.Password.RequiredLength = 6;
			options.Password.RequireNonAlphanumeric = false;
			options.Password.RequireDigit = false;
		}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


		builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
		
		var app = builder.Build();
		

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Home/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseRouting();

		app.UseAuthorization();

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}");

		app.Run();
	}
}