using libraryApi.Data;
using Microsoft.AspNetCore.Mvc;
using libraryApi.Interfaces;
using libraryApi.DTOs.Library;
using libraryApi.Repository;
using libraryApi.Mappers;
using libraryApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

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

        [HttpGet("{id}", Name = "GetBookById")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var book = await _libraryRepo.GetByIdAsync(id);

                    var bookDto = new BooksDto
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Author = book.Author,
                        Genre = book.Genre,
                        Year = book.Year,
                        Description = book.Description,
                        isAvailable = book.isAvailable,
                        Links = new List<LinkDto>
                        {
                            new LinkDto
                            {
                                Href = Url.Link("GetBookById", new {id = book.Id}),
                                Rel = "self",
                                Method = "GET"
                            },
                            new LinkDto
                            {
                                Href = Url.Link("UpdateBook", new {id = book.Id}),
                                Rel = "update-book",
                                Method = "PUT"
                            },
                            new LinkDto
                            {
                                Href = Url.Link("DeleteBook", new {id = book.Id}),
                                Rel = "delete-book",
                                Method = "DELETE"
                            }
                        }
                    };

                    return Ok(bookDto);   
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        [Route("{id}", Name = "UpdateBook")]
        [Authorize(Roles = "Admin")]
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

        [HttpDelete("{id}", Name = "DeleteBook")]
        [Authorize(Roles = "Admin")]
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
