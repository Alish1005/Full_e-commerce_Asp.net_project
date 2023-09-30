using Microsoft.EntityFrameworkCore;
using Project.Models;
using System.Collections.Generic;

namespace Project.Data.Repository
{
	public class RepoOrder : IRepoOrder
	{
		AppDbContext _db;
		
		public RepoOrder(AppDbContext db)
		{
			_db = db;
			
		}
		public async Task AddAsync(Order obj)
		{
			obj.Username= _db.users.Where(n=>n.UserName==obj.Username).ToList()[0].Id;
			
				await _db.orders.AddAsync(obj);
				//find the item object and decr its quant 
				var item = await _db.items.FindAsync(obj.ItemId);
				item.quantity -= obj.itemQuantity;
				_db.Update(item);
				await _db.SaveChangesAsync();
			
		}

		public async Task DeleteAsync(int id)
		{
			var order = await _db.orders.FindAsync(id);
			_db.orders.Remove(order);
			var item = await _db.items.FindAsync(order.ItemId);
			item.quantity += order.itemQuantity;
			_db.Update(item);
			await _db.SaveChangesAsync();
		}

		public async Task Done(int id)
		{
			var order = await _db.orders.FindAsync(id);
			_db.orders.Remove(order);
			await _db.SaveChangesAsync();
		}


		public async Task<IEnumerable<Order>> GetAllAsync()
		{
		   var all = await _db.orders.ToListAsync();
			
			for (var i=0;i<all.Count;i++)
			{
				
				all[i].item = await _db.items.Include(x => x.orders).FirstOrDefaultAsync(x => all[i].ItemId == x.Id);
				
				all[i].user = await _db.users.Include(x => x.Orders).FirstOrDefaultAsync(x => all[i].Username == x.Id);
			}
			return all;
			
		}

		public async Task<IEnumerable<Order>> GetUserOrdersAsync(string username)
		{
			var all = await _db.orders.Where(n=>n.user.UserName==username).ToListAsync();
			
			for (var i = 0; i < all.Count; i++)
			{
				all[i].item = await _db.items.Include(x => x.orders).FirstOrDefaultAsync(x => all[i].ItemId == x.Id);
				all[i].user = await _db.users.Include(x => x.Orders).FirstOrDefaultAsync(x => all[i].Username == x.Id);
			}
			return all;
		}

		public async Task<Order> GetByIdAsync(int id)
		{

			var order = await _db.orders.FindAsync(id);
			order.item = await _db.items.Include(x => x.orders).FirstOrDefaultAsync(x => order.ItemId == x.Id);
			order.user = await _db.users.Include(x => x.Orders).FirstOrDefaultAsync(x => order.Username == x.Id);
			return order;
		}


		public async Task<Order> UpdateAsync(int id, Order obj)
		{
			_db.Update(obj);
			await _db.SaveChangesAsync();
			return obj;

		}

		public async Task<IEnumerable<Order>> GetSearchAsync(string input)
		{
			var all = await _db.orders.Where(n => n.user.UserName == input).ToListAsync();

			for (var i = 0; i < all.Count; i++)
			{
				all[i].item = await _db.items.Include(x => x.orders).FirstOrDefaultAsync(x => all[i].ItemId == x.Id);
				all[i].user = await _db.users.Include(x => x.Orders).FirstOrDefaultAsync(x => all[i].Username == x.Id);
			}
			return all;
		}
	}
}
