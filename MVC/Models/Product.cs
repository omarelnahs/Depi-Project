using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [Url]
        public string? ImageUrl { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int Stock { get; set; }

        // Relationships
        public List<ProductCategory> ProductCategories { get; set; } = new();

        public int StoreId { get; set; }
        [ForeignKey("StoreId")]
        public required Store Store { get; set; }
    }
}