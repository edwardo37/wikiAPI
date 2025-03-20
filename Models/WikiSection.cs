namespace wikiAPI.Models
{
    public class WikiSection
    {
        public int ID { get; set; }

        public required string Header { get; set; }
        public required List<string> Bodies { get; set; }
    }
}