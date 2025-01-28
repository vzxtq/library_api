using System.ComponentModel.DataAnnotations.Schema;

namespace libraryApi.Models
{
    public class Books
    {
        public int Id { get; set; }
        public string Title { get; set; }= string.Empty;
        public string Author { get; set; }= string.Empty;
        public string Genre { get; set; }= string.Empty;
        public int Year { get; set; }
        public string Description { get; set; } = string.Empty;
        public string isAvailable { get; set; } = string.Empty;
    }
}