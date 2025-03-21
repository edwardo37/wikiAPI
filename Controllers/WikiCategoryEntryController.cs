using Microsoft.AspNetCore.Mvc;
using wikiAPI.Controllers.Requests;
using wikiAPI.Models;
using wikiAPI.Repositories;

namespace wikiAPI.Controllers
{
    [Route("/Wiki/Category/{CategoryID}/Entry")]
    [ApiController]
    public class WikiCategoryEntryController : ControllerBase
    {
        private readonly IWikiRepository wikiRepository;

        public WikiCategoryEntryController(IWikiRepository repository)
        {
            wikiRepository = repository;
        }

        [HttpPost("", Name = "CreateWikiEntry")]
        public WikiEntry CreateWikiEntry([FromRoute] int CategoryID, WikiEntryCreateRequest request)
        {
            WikiEntry newWikiEntry = new WikiEntry
            {
                Title = request.Title,
                Description = request.Description,

                Stats = request.Stats,
                Sections = request.Sections,

                WikiCategoryID = CategoryID
            };

            return wikiRepository.CreateEntry(newWikiEntry);
        }

        [HttpGet("", Name = "GetWikiEntriesByCategoryID")]
        public List<WikiEntry>? GetWikiEntriesByCategoryID([FromRoute] int CategoryID)
        {
            return wikiRepository.GetEntriesByCategoryID(CategoryID);
        }
    }
}