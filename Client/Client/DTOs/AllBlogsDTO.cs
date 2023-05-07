namespace Client.DTOs
{
    public class AllBlogsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateOnly Date { get; set; }
        public string Data { get; set; }
        public bool IsYour { get; set; }
        public string Username { get; set; }
    }
}
