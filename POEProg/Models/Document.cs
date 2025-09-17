using System.ComponentModel.DataAnnotations;

namespace POEProg.Models
{
    public class Document
    {
        [Key]
        public int DocumentId { get; set; }
        public int ClaimId { get; set; }
        public string FileName { get; set; }
        public DateTime UploadedDate { get; set; }

        public Claim Claim { get; set; }

    }
}
