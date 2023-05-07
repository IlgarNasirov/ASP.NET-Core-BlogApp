using System.ComponentModel.DataAnnotations;

namespace Client.DTOs
{
    public class LoginUserDTO
    {
        [Required]
        [MinLength(5, ErrorMessage = "The username field must be at least 5 characters long.")]
        [MaxLength(30, ErrorMessage = "The username field must be at most 30 characters long.")]
        public string Username { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "The password field must be at least 6 characters long.")]
        public string Password { get; set; }
    }
}