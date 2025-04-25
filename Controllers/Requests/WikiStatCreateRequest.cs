using System.ComponentModel.DataAnnotations;

namespace wikiAPI.Controllers.Requests
{
    public class WikiStatCreateRequest
    {
        [Required]
        [Length(1, 128, ErrorMessage = "Key must be between 1-128 characters")]
        public required string Key { get; set; }


        [Required]
        [Length(1, 128, ErrorMessage = "Value must be between 1-128 characters")]
        public required string Val { get; set; }
    }
}