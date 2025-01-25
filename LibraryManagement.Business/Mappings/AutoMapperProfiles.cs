using AutoMapper;
using LibraryManagement.Business.Models.Domain;
using LibraryManagement.Business.Models.DTO;

namespace LibraryManagement.Business.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<AddBookDto, Book>();
            CreateMap<UpdateBookDto, Book>();
            CreateMap<Member, MemberDto>().ReverseMap();
            CreateMap<AddMemberDto, Member>();
            CreateMap<UpdateMemberDto, Member>();
        }
    }
}