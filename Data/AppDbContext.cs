
using ExpenseTrackerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options ) : base(options)
        {
            OnModelCreating(new ModelBuilder());    
        }

        public DbSet<ExpenseTrackerApp.Models.User> Users { get; set; } = null!;
        public DbSet<ExpenseTrackerApp.Models.Product> Products { get; set; } = null!;
        public DbSet<ExpenseTrackerApp.Models.Expense> Expenses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, UserName = "alice"},
                new User { Id = 2, UserName = "bob" }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Milk", Category = "Groceries", Description = "1L skim milk", Price = 2.50M },
                new Product { Id = 2, Name = "Shampoo", Category = "Toiletries", Description = "Anti-dandruff", Price = 6.99M },
                new Product { Id = 3, Name = "Bread", Category = "Groceries", Description = "Whole grain", Price = 3.20M }
            );

            // Seed Expenses (tied to Users + Products)
            modelBuilder.Entity<Expense>().HasData(
                new Expense { Id = 1, ProductId = 1, Quantity = 3, Amount = 2.50M, Date = new DateTime(2025, 09, 01),  UserId = 1, Category = "Grocery"},
                new Expense { Id = 2, ProductId = 2, Quantity = 2, Amount = 6.99M, Date = new DateTime(2025, 09, 01),  UserId = 2, Category = "Misc" },
                new Expense { Id = 3, ProductId = 3, Quantity = 1, Amount = 3.20M, Date = new DateTime(2025, 09, 01), UserId = 1 , Category = "Grocery"}
            );
        }
    }
}
