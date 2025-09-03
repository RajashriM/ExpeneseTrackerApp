using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ExpenseTrackerApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public required string Name { get; set; }
        public string? Description { get; set; }

        [MaxLength(50)]
        public required string Category { get; set; }

        [Required, Precision(18, 2)]
        public required decimal Price { get; set; }

    }
}
