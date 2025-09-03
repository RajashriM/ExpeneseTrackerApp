using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ExpenseTrackerApp.Models
{
    public class User
    {
        public int Id {get; set; }

        public required string UserName { get; set; }
        
        public string? PasswordHash { get; set; }

        public string Role { get; set; } = "User";

    }
}
