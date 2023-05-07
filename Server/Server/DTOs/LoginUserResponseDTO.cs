namespace Server.DTOs
{
    public class LoginUserResponseDTO
    {
        public string? Message { get; set; }
        public bool Type { get; set; }
        public int Id { get; set; }
        public string? Username { get; set; }
    }
}