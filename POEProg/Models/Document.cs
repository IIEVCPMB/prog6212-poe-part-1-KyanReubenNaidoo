using System.ComponentModel.DataAnnotations;

namespace POEProg.Models
{
    public class Document
    {
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; } = string.Empty;
        public string? EncryptedName { get; set; }
        public double Size { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.UtcNow;
    }
}
