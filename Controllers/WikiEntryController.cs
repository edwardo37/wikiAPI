using Microsoft.AspNetCore.Mvc;
using wikiAPI.Models;
using wikiAPI.Repositories;

namespace wikiAPI.Controllers
{
    [Route("/w/Entry")]
    [ApiController]
    public class WikiEntryController : ControllerBase
    {
        private readonly IWikiRepository wikiRepository;

        public WikiEntryController(IWikiRepository repository)
        {
            wikiRepository = repository;
        }

        [HttpGet("{EntryID}", Name = "GetWikiEntry")]
        public WikiEntry? GetWikiEntry(
            int EntryID,
            [FromQuery] bool includeStats = true, [FromQuery] bool includeSections = true,
            [FromQuery] bool includeCategoryData = false
            )
        {
            return wikiRepository.GetEntryByID(EntryID, includeStats, includeSections, includeCategoryData);
        }
    }
}