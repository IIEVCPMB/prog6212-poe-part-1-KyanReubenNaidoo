using System.ComponentModel.DataAnnotations;

namespace POEProg.Models
{
    public class Approval
    {
        [Key]

        public int ApprovalId { get; set; }

        public string ApprovedBy { get; set; }

        public string Role { get; set; }

        public string status    { get; set; }

        public int ClaimId { get; set; }

        public Claim Claim { get; set; }
    }
}
