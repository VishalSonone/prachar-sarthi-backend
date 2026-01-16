using System.ComponentModel.DataAnnotations;

namespace PracharSaarathi.Api.Models
{
    public class Karyakarta
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;
        
        [Required]
        public DateTime DateOfBirth { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Constituency { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
