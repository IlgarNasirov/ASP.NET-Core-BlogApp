using System.ComponentModel.DataAnnotations;

namespace Client.DTOs
{
    public class AddBlogDTO
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "The title field must be at least 3 characters long.")]
        [MaxLength(30, ErrorMessage = "The title field must be at most 30 characters long.")]
        public string Title { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "The data field must be at least 5 characters long.")]
        public string Data { get; set; }
        public int UserId { get; set; }
    }
}