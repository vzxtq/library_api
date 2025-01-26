using libraryApi.Data;
using Microsoft.AspNetCore.Mvc;
using libraryApi.Interfaces;
using libraryApi.DTOs.Library;
using libraryApi.Repository;
using libraryApi.Mappers;
using libraryApi.Models;

namespace libraryApi.Controllers 
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase 
    {
        private readonly ApplicationDbContext _context;
        private readonly ILibraryRepo _libraryRepo;    

        public BookController(ApplicationDbContext context, ILibraryRepo libraryRepo)
        {
            _context = context;
            _libraryRepo = libraryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var books = await _libraryRepo.GetAllAsync();
                var bookDtos = books.Select(book => book.ToLibraryDto());
                return Ok(bookDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var book = await _libraryRepo.GetByIdAsync(id);

                if (book == null)
                {
                    return NotFound();
                }
                return Ok(book.ToLibraryDto());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBooksDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var bookModel = createDto.ToLibraryFromCreateDto();
                await _libraryRepo.CreateAsync(bookModel);
                return CreatedAtAction(nameof(GetById), new { id = bookModel.Id }, bookModel.ToLibraryDto());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBooksDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingBook = await _libraryRepo.GetByIdAsync(id);
                if (existingBook == null)
                {
                    return NotFound();
                }

                var book = updateDto.ToLibraryFromUpdateDto(existingBook);
                await _libraryRepo.UpdateAsync(id, book);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var deletedBook = await _libraryRepo.DeleteAsync(id);
                if (deletedBook == null)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
