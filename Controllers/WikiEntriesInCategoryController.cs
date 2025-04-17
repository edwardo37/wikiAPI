using Microsoft.AspNetCore.Mvc;
using wikiAPI.Controllers.Requests;
using wikiAPI.Models;
using wikiAPI.Repositories;

namespace wikiAPI.Controllers
{
    [Route("/Wiki/Categories/{CategoryID}/Entries")]
    [ApiController]
    public class WikiEntriesInCategoryController : ControllerBase
    {
        private readonly IWikiRepository wikiRepository;

        public WikiEntriesInCategoryController(IWikiRepository repository)
        {
            wikiRepository = repository;
        }

        // Create a new wiki entry in a category, specified by the category ID
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

        // Get all the entries in a category, by category ID
        [HttpGet("", Name = "GetWikiEntriesByCategoryID")]
        public List<WikiEntry>? GetWikiEntriesByCategoryID([FromRoute] int CategoryID)
        {
            return wikiRepository.GetEntriesByCategoryID(CategoryID);
        }
    }
}