using System;
using System.Collections.Generic;

namespace MVC.Models;

    public class Store
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;

        // Foreign Key
        public int UserId { get; set; }

        // Navigation
        public User? User { get; set; }
        public ICollection<Product>? Products { get; set; }
    }

