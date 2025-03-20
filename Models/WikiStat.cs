namespace wikiAPI.Models
{
    public class WikiStat
    {
        public int ID { get; set; }

        public required string Key { get; set; }
        public required string Val { get; set; }
    }
}