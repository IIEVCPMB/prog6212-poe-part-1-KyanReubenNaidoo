using System.ComponentModel.DataAnnotations;

namespace POEProg.Models
{
    public class Lecturer
    {
        [Key]
        public int LecturerId { get; set; }
        public string FullName { get; set; }
        
        public string Email { get; set; }
        
       
    }
}
