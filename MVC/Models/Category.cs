using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class Category
{
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    // Relationships
    public List<ProductCategory> ProductCategories { get; set; } = new();
}
