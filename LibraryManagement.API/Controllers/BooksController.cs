using AutoMapper;
using LibraryManagement.Business.Interfaces;
using LibraryManagement.Business.Models.Domain;
using LibraryManagement.Business.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BooksController(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddBookDto addBookDto)
        {
            // Map or Convert DTO to Domain Model
            var bookDomainModel = _mapper.Map<Book>(addBookDto);

            // Use Domain Model to create Artist
            bookDomainModel = await _unitOfWork.Books.CreateAsync(bookDomainModel);
            await _unitOfWork.SaveAsync();

            // Map Domain Model back to DTO
            var songRequestDto = _mapper.Map<BookDto>(bookDomainModel);


            // Show information to the client
            return Ok(songRequestDto);
            //return CreatedAtAction(nameof(GetById), new { id = songRequestDto.Id }, songRequestDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get all song requests
            var bookDomainModel = await _unitOfWork.Books.GetAllAsync();

            // Map Domain Model to DTO
            var booksDto = _mapper.Map<List<BookDto>>(bookDomainModel);

            // Return DTO to the client
            return Ok(booksDto);
        }
    }
}