using libraryApi.Data;
using libraryApi.Interfaces;
using libraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace libraryApi.Repository
{
    public class LibraryRepo : ILibraryRepo
    {
        private readonly ApplicationDbContext _context;
        public LibraryRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Books> CreateAsync(Books bookModel)
        {
            await _context.Books.AddAsync(bookModel);
            await _context.SaveChangesAsync();
            return bookModel;
        }

        public async Task<Books?> DeleteAsync(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(x=> x.Id == id);
            if (book == null)
            {
                return null;
            }
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<List<Books>> GetAllAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Books?> GetByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<Books?> UpdateAsync(int id, Books book)
        {
            var existingBook = await _context.Books.FindAsync(book.Id);
            if (existingBook == null)
            {
                return null;
            }
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Genre = book.Genre;
            existingBook.Description = book.Description;
            await _context.SaveChangesAsync();
            return existingBook;
        }
    }
}