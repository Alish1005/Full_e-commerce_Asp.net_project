using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Data.Repository;
using Project.Models;

namespace Project.Controllers
{
	public class OrderController : Controller
	{

		
		private readonly IRepoOrder _OrderRepo;

		public OrderController(IRepoOrder orderRepo)
		{
			_OrderRepo = orderRepo;
		}
		[Authorize]
		public async Task<IActionResult> Index(string id)
		{
		   var all = await _OrderRepo.GetUserOrdersAsync(id);
			return View(all);
		}


		//add item to the cart In the Menu + i can edit the data base from here not from the cart Class
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> AddOrder(Order order,string id,int id2)
		{
			
			//!!!!!!!!!!!THe id must be user id and not the username!!!!!!!!
			order.Username = id;
			order.ItemId = id2;
			await _OrderRepo.AddAsync(order);
			
			return Redirect("http://localhost:5001/Menu");
		}
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> DeleteOrder(int id)
		{
			//!!!!!!!!!!!THe id must be user id and not the username!!!!!!!!
			await _OrderRepo.DeleteAsync(id);
			return Redirect("http://localhost:5001/Order/Index/" + User.Identity.Name);
		}
		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<IActionResult> Done(int id)
		{
			//!!!!!!!!!!!THe id must be user id and not the username!!!!!!!!
			await _OrderRepo.Done(id);
			return Redirect("http://localhost:5001/Order/AllOrders");
		}
		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<IActionResult> Search(string id)
		{
			if(id == null||id==" ")
			{
				return RedirectToAction(nameof(AllOrders));
			}
			//!!!!!!!!!!!The id must be user id and not the username!!!!!!!!
			var all = await _OrderRepo.GetSearchAsync(id);
			return View(all);
		}
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> AllOrders()
		{
			var all=await _OrderRepo.GetAllAsync();
			return View(all);
		}
		
	}
}
