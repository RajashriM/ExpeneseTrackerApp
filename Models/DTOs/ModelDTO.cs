namespace ExpenseTrackerApp.Models.DTOs
{
    public class UserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class ProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }

    public class ExpenseDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
