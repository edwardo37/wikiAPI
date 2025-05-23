using System.Text.Json.Serialization;

namespace wikiAPI.Models
{
    public class WikiStat
    {
        public int ID { get; set; }

        public required string Key { get; set; }
        public required string Val { get; set; }

        // For reference in code
        [JsonIgnore]
        public int WikiEntryID { get; set; }
    }
}