using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Business.Models.DTO
{
    public class ReturnBookDto
    {
        [Required]
        public int BookId { get; set; }
        [Required]
        public int MemberId { get; set; }
    }
}