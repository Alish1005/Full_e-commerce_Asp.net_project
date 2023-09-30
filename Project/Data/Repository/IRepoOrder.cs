using Project.Models;

namespace Project.Data.Repository
{
	public interface IRepoOrder
	{
		Task<IEnumerable<Order>> GetAllAsync();
		Task<Order> GetByIdAsync(int id);

        Task<IEnumerable<Order>> GetSearchAsync(string input);
        Task AddAsync(Order obj);
		Task Done(int id);

        Task<Order> UpdateAsync(int id, Order obj);
		Task<IEnumerable<Order>> GetUserOrdersAsync(string username);
		Task DeleteAsync(int id);
	}
}
