using System.ComponentModel.DataAnnotations;

namespace POEProg.Models
{
    public class Claim
    {
        public int Id { get; set; }

        [Required]
        public string LecturerName { get; set; }

        [Required]
        [Range(1, 180, ErrorMessage = "Hours worked cannot exceed 180.")]
        public double HoursWorked { get; set; }

        [Required]
        public double HourlyRate { get; set; }

        public double Total { get; set; }
        public string? Notes { get; set; }
        public DateTime DateSubmitted { get; set; } = DateTime.UtcNow;
        public ClaimStatus Status { get; set; } = ClaimStatus.Pending;
        public List<Document> Documents { get; set; } = new();
    }
}


