using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Business.Models.DTO
{
    public class UpdateBookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}