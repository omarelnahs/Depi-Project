using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    public class ProductCategory
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }

}
