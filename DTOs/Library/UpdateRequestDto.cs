namespace libraryApi.DTOs.Library
{
    public class UpdateBooksDto
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Description { get; set; } = string.Empty;
        public string isAvailable { get; set; } = string.Empty;
    }
}