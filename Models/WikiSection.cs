using System.Text.Json.Serialization;

namespace wikiAPI.Models
{
    public class WikiSection
    {
        public int ID { get; set; }

        public required string Header { get; set; }
        public required List<string> Bodies { get; set; }

        // Not needed in the API, but needed for the DB
        [JsonIgnore]
        public int WikiEntryID { get; set; }
    }
}