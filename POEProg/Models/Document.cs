using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POEProg.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "File name is required")]
        [StringLength(255, ErrorMessage = "File name cannot exceed 255 characters")]
        public string FileName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Encrypted file name is required")]
        [StringLength(255)]
        public string EncryptedName { get; set; } = string.Empty;

        [Required]
        [Range(1, 5_000_000, ErrorMessage = "File size must be between 1 byte and 5MB")]
        public long Size { get; set; }

        [Required]
        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

        // Foreign key to Claim
        [Required]
        public int ClaimId { get; set; }

        [ForeignKey("ClaimId")]
        public Claim? Claim { get; set; }
    }
}
