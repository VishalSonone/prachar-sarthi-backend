using System.ComponentModel.DataAnnotations;

namespace PracharSaarathi.Api.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;
        
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
    }
}
