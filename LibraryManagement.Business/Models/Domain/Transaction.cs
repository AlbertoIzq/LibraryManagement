namespace LibraryManagement.Business.Models.Domain
{
    public class Transaction : BaseEntity
    {
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public DateTime BorrowedAt { get; set; }
        public DateTime? ReturnedAt { get; set; }

        // Navigation properties
        public Book Book { get; set; }
        public Member Member { get; set; }
    }
}