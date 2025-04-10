namespace wikiAPI.Models
{
    // Each category has a name, a description, and a list of entries
    // As you'll see, it trickles down from category to entry
    public class WikiCategory
    {
        public int ID { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }

        public List<WikiEntry> WikiEntries { get; set; } = [];
    }
}