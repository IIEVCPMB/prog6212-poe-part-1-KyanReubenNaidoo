using System.ComponentModel.DataAnnotations;

namespace POEProg.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Surname { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Range(1, 500)]
        public double HourlyRate { get; set; }

        [Required]
        public string Role { get; set; } = "Lecturer"; // Lecturer, Coordinator, Manager, HR

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
