using System.ComponentModel.DataAnnotations;

namespace POEProg.Models
{
    public class Claim
    {
        public int Id { get; set; }

        [Required]
        public string LecturerName { get; set; }

        [Required]
        [Range(1,500, ErrorMessage = "Hours worked must be between 1 to 500")]
        public double HoursWorked { get; set; }

        [Required]
        [Range(1, 500, ErrorMessage = "Hourly Rate must be between 1 to 500")]
        public double HourlyRate { get; set; }
        public string? Notes { get; set; }
        public DateTime DateSubmitted { get; set; } = DateTime.Now;
        public ClaimStatus Status { get; set; } = ClaimStatus.Pending;
        public List<Document> Documents { get; set; } = new();  
    }
}
