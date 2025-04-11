﻿
using System.ComponentModel.DataAnnotations;
namespace MVC.Models
{ 
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        public Cart Cart { get; set; } 
        public Product Product { get; set; }
    }
}