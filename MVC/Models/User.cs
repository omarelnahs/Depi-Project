using System;
using System.Collections.Generic;

namespace MVC.Models;

    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;

        // Navigation
        public ICollection<Store>? Stores { get; set; }
        public ICollection<Order>? Orders { get; set; }
}

