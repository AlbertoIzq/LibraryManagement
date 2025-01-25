using AutoMapper;
using LibraryManagement.Business.Interfaces;
using LibraryManagement.Business.Models.Domain;
using LibraryManagement.Business.Models.DTO;
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

            // Use Domain Model to create Book
            bookDomainModel = await _unitOfWork.Books.CreateAsync(bookDomainModel);
            await _unitOfWork.SaveAsync();

            // Map Domain Model back to DTO
            var bookDto = _mapper.Map<BookDto>(bookDomainModel);

            // Show information to the client
            return CreatedAtAction(nameof(GetById), new { id = bookDto.Id }, bookDto);
        }

        // GET: api/songs?filterOn=PropertyName&filterQuery=PropertyValue
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery)
        {
            // Get all books
            var booksDomainModel = await _unitOfWork.Books.GetAllAsync(filterOn, filterQuery);

            // Map Domain Model to DTO
            var booksDto = _mapper.Map<List<BookDto>>(booksDomainModel);

            // Return DTO to the client
            return Ok(booksDto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            // Get data from database - Domain Model
            var bookDomainModel = await _unitOfWork.Books.GetByIdAsync(id);

            if (bookDomainModel == null)
            {
                return NotFound();
            }

            // Convert Domain Model to DTO
            var bookDto = _mapper.Map<BookDto>(bookDomainModel);

            // Return DTO back to client
            return Ok(bookDto);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBookDto updateBookDto)
        {
            // Map DTO to Domain Model
            var bookDomainModel = _mapper.Map<Book>(updateBookDto);

            // Update book if it exists
            bookDomainModel = await _unitOfWork.Books.UpdateAsync(id, bookDomainModel);
            await _unitOfWork.SaveAsync();

            if (bookDomainModel == null)
            {
                return NotFound();
            }

            // Convert Domain Model to DTO
            var bookDto = _mapper.Map<BookDto>(bookDomainModel);

            return Ok(bookDto);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            // Delete book if it exists
            var bookDomainModel = await _unitOfWork.Books.DeleteAsync(id);
            await _unitOfWork.SaveAsync();

            if (bookDomainModel == null)
            {
                return NotFound();
            }

            // Return No content back to client
            return NoContent();
        }
    }
}