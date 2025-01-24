using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Business.Models.DTO
{
    public class AddBookDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public bool IsAvailable { get; set; } = true;
    }
}