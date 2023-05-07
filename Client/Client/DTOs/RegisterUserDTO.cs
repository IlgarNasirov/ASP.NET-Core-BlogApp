using System.ComponentModel.DataAnnotations;

namespace Client.DTOs
{
    public class RegisterUserDTO
    {
        [Required]
        [MinLength(5, ErrorMessage = "The full name field must be at least 5 characters long.")]
        [MaxLength(60, ErrorMessage = "The full name field must be at most 60 characters long.")]
        public string FullName { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "The username field must be at least 5 characters long.")]
        [MaxLength(30, ErrorMessage = "The username field must be at most 30 characters long.")]
        public string Username { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "The password field must be at least 6 characters long.")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "The repassword field must be equal to the password field.")]
        public string RePassword { get; set; }
    }
}