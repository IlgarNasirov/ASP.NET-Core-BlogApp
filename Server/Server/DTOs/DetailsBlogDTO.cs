namespace Server.DTOs
{
    public class DetailsBlogDTO
    {
        public string Title { get; set; }
        public DateOnly Date { get; set; }
        public string Data { get; set; }
        public string Username { get; set; }
        public int Views { get; set; }
    }
}