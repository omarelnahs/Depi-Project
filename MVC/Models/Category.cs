using System;
using System.Collections.Generic;

namespace MVC.Models;

public partial class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
