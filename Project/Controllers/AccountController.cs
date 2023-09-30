using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project.Data.Repository;
using Project.Models;
using Project.Models.Account;
using System.Net;

namespace Project.Controllers
{
	
	public class AccountController : Controller
	{

		private readonly IEntityRepository<User> _UserRepo;
		private UserManager<User> userManager;
		private SignInManager<User> signInManager;
		public AccountController(UserManager<User> userMngr,
		SignInManager<User> signInMngr)
		{
			userManager = userMngr;
			signInManager = signInMngr;
		
		}


		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Index()
		{
			return View();
		}
		[AllowAnonymous]
		//Register
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
		[AllowAnonymous]
		[HttpPost]
		public async Task<IActionResult> Register(RegisterVM model)
		{
			if (ModelState.IsValid)
			{
				var user = new User {
					UserName = model.Username ,
					Address=model.Address ,
					Email = model.Email ,
					PhoneNumber=model.Phone
				};
				var result = await userManager.CreateAsync(user,
				model.Password);
				if (result.Succeeded)
				{
					await signInManager.SignInAsync(user,
					isPersistent: false);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}
				}
			}
			return View(model);
		}
		[Authorize]
		//Logout
		[HttpPost]
		public async Task<IActionResult> LogOut()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
		[AllowAnonymous]
		//Login
		[HttpGet]
		public IActionResult LogIn(string returnURL = "")
		{
			var model = new LoginVM { ReturnUrl = returnURL };
			return View(model);
		}
		[AllowAnonymous]
		[HttpPost]
		public async Task<IActionResult> LogIn(LoginVM model)
		{
			if (/*ModelState.IsValid*/ !string.IsNullOrEmpty(model.Username) && !string.IsNullOrEmpty(model.Password))
			{
				var result = await signInManager.PasswordSignInAsync(
				model.Username, model.Password,
				isPersistent: model.RememberMe,
				lockoutOnFailure: false);
				/*			if (userManager.Users.Where(n => n.UserName.Equals(User.Identity.Name)).First().isBan)
							{
								return Redirect("http://localhost:5001/Managment/BanUser");

							}		*/
				if (result.Succeeded)
				{
					if (!string.IsNullOrEmpty(model.ReturnUrl) &&
					Url.IsLocalUrl(model.ReturnUrl))
					{
						return Redirect(model.ReturnUrl);
					}
					else
					{
						return RedirectToAction("Index", "Home");
					}
				}
			}
			ModelState.AddModelError("", "Invalid username/password.");
			return View(model);
		}
		public ViewResult AccessDenied()
		{
			return View();
		}
	   
	}
}
