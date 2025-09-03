using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerApp.Models
{
    public class Expense
    {
        public long Id { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }

        public required int Quantity { get; set; }

        [Required, Precision(18, 2)]
        public required decimal Amount { get; set; }
        public required DateTime Date { get; set; } = DateTime.Now;

        [MaxLength(50)]
        public required string Category { get; set; }

    }
}
