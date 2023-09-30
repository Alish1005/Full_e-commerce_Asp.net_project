using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string IconUrl { get; set; }
        [Required]
        public int quantity { get; set; } = 1;
        
        public List<Order> orders { get; set; }

        public bool OFS()
        {
            return quantity > 0;
        }
        //relations
       // List<item_order> items_orders { get; set; }

    }
}
