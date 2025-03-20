using System.ComponentModel.DataAnnotations;

namespace wikiAPI.Controllers.Requests
{
    public class WikiCategoryCreateRequest
    {
        [Required]
        [Length(1, 128, ErrorMessage = "Title must be between 1-128 characters")]
        public required string Title { get; set; }

        [MaxLength(512, ErrorMessage = "A short description must be less than 512 characters")]
        public string? Description { get; set; }
    }
}