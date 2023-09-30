using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Data.Repository;
using Project.Models;
using Project.Models.Account;
using System.Collections;
using System.Data;

namespace Project.Controllers
{
	public class MenuController : Controller
	{
		private readonly IEntityRepository<Item> _ItemRepo;
		
		public MenuController(IEntityRepository<Item> ItemRepo)
		{
			_ItemRepo = ItemRepo;
			
		}
		//Go to Menu
		public async Task<IActionResult> Index()
		{
			var all = await _ItemRepo.GetAllAsync();
			return View(all);
		}



		//Not now !!! Go to item Detail
		[Authorize]
		[HttpGet]
		public async Task<IActionResult> Add_Order(int id)
		{
			var order=new Order();
			var item = await _ItemRepo.GetByIdAsync(id);
			order.item = item;
			order.ItemId = id;
			order.note = "";
			order.itemQuantity = 1;
			order.Username = User.Identity.Name;
			return View(order);
		}

	}
}
