using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Data.Repository;
using Project.Models.Account;
using System.Data;

namespace Project.Controllers
{
	
	public class ManagmentController : Controller
	{
		private readonly IEntityRepository<User> _UserRepo;
		public ManagmentController(IEntityRepository<User> UserRepo)
		{
			_UserRepo = UserRepo;
		}
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Index()
		{
			var users= await _UserRepo.GetAllAsync();
			return View(users);
		}
	/*	[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<IActionResult> BanUser(string id)
		{
			var users =await _UserRepo.GetAllAsync();
			var user = new User();
			foreach (var i in users)
			{
				if (i.UserName.Equals(id))
				{
					user = i;
					break;
				}
			}
			if (user.isBan)
			{
				user.isBan = false;
			}
			else
			{
				user.isBan = true;
			}
			await _UserRepo.UpdateAsync(user.Id, user);
			return RedirectToAction(nameof(Index));
		}
		[AllowAnonymous]
		public async Task<IActionResult> BanUser()
		{

			return View();
		}
	*/
	}
}
