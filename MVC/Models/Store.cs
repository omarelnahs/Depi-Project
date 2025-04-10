using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

    public class Store
    {
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;

    // Relationships
    public int UserId { get; set; }  // Store owner
    public User User { get; set; }
    public List<Product> Products { get; set; } = new List<Product>();
}

