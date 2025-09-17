using System.ComponentModel.DataAnnotations;

namespace POEProg.Models
{
    public class Claim
    {
        [Key]
        public int ClaimId { get; set; }
        public int LecturerId { get; set; }
        public int Month {  get; set; }
        public double TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime SubmissionDate { get; set; }
        
        public Lecturer Lecturer { get; set; }
         
    }
}
