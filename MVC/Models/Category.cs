using System;
using System.Collections.Generic;

namespace MVC.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Relationships
    public List<Product> Products { get; set; }
}
