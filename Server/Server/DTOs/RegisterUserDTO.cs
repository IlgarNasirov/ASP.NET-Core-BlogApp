using System.ComponentModel.DataAnnotations;

namespace Server.DTOs
{
    public class RegisterUserDTO
    {
        [Required]
        [MinLength(5)]
        [MaxLength(60)]
        public string FullName { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(30)]
        public string Username { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Compare("Password")]
        public string RePassword { get; set; }
    }
}
