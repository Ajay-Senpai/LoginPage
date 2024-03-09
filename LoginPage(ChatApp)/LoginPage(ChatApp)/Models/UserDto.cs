using System.ComponentModel.DataAnnotations;

namespace LoginPage_ChatApp_.Models
{
    public class UserDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
