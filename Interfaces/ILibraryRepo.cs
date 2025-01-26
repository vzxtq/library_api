using libraryApi.Models;

namespace libraryApi.Interfaces
{
    public interface ILibraryRepo
    {
        Task<List<Books>> GetAllAsync();
        Task<Books?> GetByIdAsync(int id);
        Task<Books> CreateAsync(Books bookModel);
        Task<Books?> UpdateAsync(int id, Books bookDto);
        Task<Books?> DeleteAsync(int id);

    }
}