using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class User
{
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(255)]
    public string PasswordHash { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public bool IsAdmin { get; set; }

    // Relationships
    public Cart Cart { get; set; }

    // Other relationships
    public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
}

   