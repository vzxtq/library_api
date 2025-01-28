using Newtonsoft.Json;

namespace libraryApi.DTOs.Library
{
    public class BooksDto
    {
        public int Id { get; set; }
        public string Title { get; set; }= string.Empty;
        public string Author { get; set; }= string.Empty;
        public string Genre { get; set; }= string.Empty;
        public int Year { get; set; }
        public string Description { get; set; } = string.Empty;
        public string isAvailable { get; set; } = string.Empty;

        [JsonProperty("links")]
        public List<LinkDto> Links{ get; set; } = new List<LinkDto>();
    }
}