using System.ComponentModel.DataAnnotations;

namespace Server.DTOs
{
    public class AddBlogDTO
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Title { get; set; }
        [Required]
        [MinLength(5)]
        public string Data { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}