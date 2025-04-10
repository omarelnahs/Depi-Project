namespace MVC.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        // Relationships
        public User User { get; set; }
        public List<CartItem> Items { get; set; } = new();
    }
}