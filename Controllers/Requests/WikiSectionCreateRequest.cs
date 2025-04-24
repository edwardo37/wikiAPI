using System.ComponentModel.DataAnnotations;

namespace wikiAPI.Controllers.Requests
{
    public class WikiSectionCreateRequest
    {
        [Required]
        [Length(1, 128, ErrorMessage = "Header must be between 1-128 characters")]
        public required string Header { get; set; }


        [Required(ErrorMessage = "Every section must have at least 1 body")]
        public required List<string> Bodies { get; set; }
    }
}