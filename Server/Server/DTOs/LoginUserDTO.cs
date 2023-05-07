using System.ComponentModel.DataAnnotations;

namespace Server.DTOs
{
    public class LoginUserDTO
    {
        [Required]
        [MinLength(5)]
        [MaxLength(30)]
        public string Username { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}