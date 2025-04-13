using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(100)]
        public string? Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Store>? Stores { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public Cart? Cart { get; set; }

        // Custom roles not needed now, use Identity Roles instead
    }
}
