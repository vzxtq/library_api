using libraryApi.DTOs.Library;
using libraryApi.Models;

namespace libraryApi.Mappers
{
    public static class LibraryMappers
    {
        public static BooksDto ToLibraryDto(this Books book)
        {
            return new BooksDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                Year = book.Year,
                Description = book.Description
            };
        }
        public static Books ToLibraryFromCreateDto(this CreateBooksDto createBooksRequestDto)
        {
            return new Books
            {
                Title = createBooksRequestDto.Title,
                Author = createBooksRequestDto.Author,
                Genre = createBooksRequestDto.Genre,
                Year = createBooksRequestDto.Year,
                Description = createBooksRequestDto.Description,
                isAvailable = createBooksRequestDto.isAvailable
            };
        }
        public static Books ToLibraryFromUpdateDto(this UpdateBooksDto updateBooksDto, Books existingBook)
        {
            existingBook.Title = updateBooksDto.Title;
            existingBook.Author = updateBooksDto.Author;
            existingBook.Genre = updateBooksDto.Genre;
            existingBook.Year = updateBooksDto.Year;
            existingBook.Description = updateBooksDto.Description;
            existingBook.isAvailable = updateBooksDto.isAvailable;
            return existingBook;
        }
    }
}
