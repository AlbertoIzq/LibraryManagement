namespace LibraryManagement.Business.Models.DTO
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}