using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Business.Models.DTO
{
    public class AddMemberDto
    {
        [Required]
        public string Name { get; set; }
    }
}