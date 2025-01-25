using LibraryManagement.API.Data;
using LibraryManagement.Business.Interfaces;
using LibraryManagement.Business.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.API.Repositories
{
    public class MemberRepository : Repository<Member>, IMemberRepository
    {
        private readonly LibraryDbContext _libraryDbContext;

        public MemberRepository(LibraryDbContext libraryDbContext) : base(libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }

        public async Task<Member?> UpdateAsync(int id, Member member)
        {
            // Check if it exists
            var existingMember = await _libraryDbContext.Members.FirstOrDefaultAsync(x => x.Id == id);

            if (existingMember == null)
            {
                return null;
            }

            // Assign updated values
            existingMember.Name = member.Name;

            return existingMember;
        }
    }
}