using Microsoft.AspNetCore.Mvc;
using wikiAPI.Controllers.Requests;
using wikiAPI.Models;
using wikiAPI.Repositories;

namespace wikiAPI.Controllers
{
    [Route("/Wiki/Entries")]
    [ApiController]
    public class WikiEntryController : ControllerBase
    {
        private readonly IWikiRepository wikiRepository;

        public WikiEntryController(IWikiRepository repository)
        {
            wikiRepository = repository;
        }

        // Get wiki entry by an ID
        [HttpGet("{EntryID}", Name = "GetWikiEntry")]
        public WikiEntry? GetWikiEntry(
            int EntryID,
            [FromQuery] bool includeStats = true, [FromQuery] bool includeSections = true,
            [FromQuery] bool includeCategoryData = false
            )
        {
            return wikiRepository.GetEntryByID(EntryID, includeStats, includeSections, includeCategoryData);
        }

        // Update a wiki entry with an ID and the new data
        [HttpPut("{EntryID}", Name = "UpdateWikiEntry")]
        public WikiEntry? UpdateWikiEntry(int EntryID, WikiEntryCreateRequest request)
        {
            WikiEntry? entryToUpdate = GetWikiEntry(EntryID);

            if (entryToUpdate == null)
            {
                throw new Exception("Wiki entry not found. Cannot be updated");
            }

            entryToUpdate.Title = request.Title;
            entryToUpdate.Description = request.Title;
            entryToUpdate.Sections = request.Sections;
            entryToUpdate.Stats = request.Stats;

            return wikiRepository.UpdateEntry(entryToUpdate);
        }

        // Delete an wiki entry by its ID
        [HttpDelete("{EntryID}", Name = "DeleteWikiEntry")]
        public void DeleteWikiEntry(int EntryID)
        {
            WikiEntry? entryToDelete = GetWikiEntry(EntryID);

            if (entryToDelete == null)
            {
                throw new Exception("Wiki entry not found. Cannot be deleted");
            }

            wikiRepository.DeleteEntry(entryToDelete);
        }
    }
}