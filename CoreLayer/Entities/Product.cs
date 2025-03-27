using MathNet.Numerics;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")]
        public int Stock { get; set; }

        // Computed Property (Not Mapped)
        [NotMapped]
        public bool IsInStock => Stock > 0;

        // Foreign Key
        [Required]
        public int CategoryId { get; set; }

        // Navigation Property
        public Category? Category { get; set; }

        // Tracking fields
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Soft delete flag
        public bool IsDeleted { get; set; } = false;
    }
}
