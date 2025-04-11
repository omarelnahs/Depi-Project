﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        // Relationships
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
