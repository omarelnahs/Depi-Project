using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Relationships
    public Cart Cart { get; set; }

    // Other relationships
    public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
}

   