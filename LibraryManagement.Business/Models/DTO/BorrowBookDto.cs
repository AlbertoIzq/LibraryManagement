using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Business.Models.DTO
{
    public class BorrowBookDto
    {
        [Required]
        public int BookId { get; set; }
        [Required]
        public int MemberId { get; set; }
    }
}