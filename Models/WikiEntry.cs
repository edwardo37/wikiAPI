using System.Text.Json.Serialization;

namespace wikiAPI.Models
{
    public class WikiEntry
    {
        public int ID { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }

        // The stats in the sidebar, as key and value pairs
        public List<WikiStat>? Stats { get; set; }

        // Sections, as heading and body pairs
        public required List<WikiSection> Sections { get; set; }


        // DB relational info
        public int WikiCategoryID { get; set; }
        // For reference in code
        [JsonIgnore]
        public WikiCategory? WikiCategory { get; set; }
    }
}