using System.ComponentModel.DataAnnotations;
using wikiAPI.Models;

namespace wikiAPI.Controllers.Requests
{
    public class WikiEntryCreateRequest
    {
        [Required]
        [Length(1, 128, ErrorMessage = "Title must be between 1-128 characters")]
        public required string Title { get; set; }

        [MaxLength(512, ErrorMessage = "A short description must be less than 512 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Every entry must have at least 1 section")]
        public required List<WikiSection> Sections { get; set; }

        public List<WikiStat>? Stats { get; set; }
    }
}