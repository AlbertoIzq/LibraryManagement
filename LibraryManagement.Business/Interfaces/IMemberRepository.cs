using LibraryManagement.Business.Models.Domain;

namespace LibraryManagement.Business.Interfaces
{
    public interface IMemberRepository : IRepository<Member>
    {
        Task<Member?> UpdateAsync(int id, Member member);
    }
}