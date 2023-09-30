using Project.Models.Account;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
	public class Order
	{
		[Key]
		public int Id { get; set; }

		//relations
		[Required]
	   public string Username { get; set; }
		[ForeignKey("Username")]
		public User user { get; set;}
		
		
		//
		public int ItemId { get; set; }
		
		public Item item;
		public int itemQuantity { get; set; } = 1;
		public string? note { get; set; }

		public double totalPrice()
		{
			return item.Price*itemQuantity;
		}

	}
}
