using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Change UserId to string
        public string UserId { get; set; }

        // Relationships
        public required ApplicationUser User { get; set; }
        public List<CartItem> Items { get; set; } = new();
    }

}
