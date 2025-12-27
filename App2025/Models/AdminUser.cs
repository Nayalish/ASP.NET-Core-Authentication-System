using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace App2025.Models
{
    public class AdminUser
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("User Name")]
        public string? Name { get; set; }
        [Required]
        [DisplayName("Password")]
        [MaxLength(30, ErrorMessage = "The maximum lenght is 10")]

        public string? Password { get; set; }
    }
}
