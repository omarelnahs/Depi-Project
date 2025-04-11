using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Total { get; set; }
        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Relationships
        public User User { get; set; }
        public List<OrderItem> Items { get; set; } = new();
    }
}
