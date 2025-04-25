using Microsoft.AspNetCore.Mvc;
using wikiAPI.Controllers.Requests;
using wikiAPI.Exceptions;
using wikiAPI.Models;
using wikiAPI.Repositories;

namespace wikiAPI.Controllers
{
    [ApiController]
    public class WikiEntryController : ControllerBase
    {
        private readonly IWikiRepository wikiRepository;

        public WikiEntryController(IWikiRepository repository)
        {
            wikiRepository = repository;
        }

        // Create a new wiki entry in a category, specified by the category ID
        [HttpPost("/Wiki/Categories/{CategoryID}/Entries", Name = "CreateWikiEntry")]
        public WikiEntry CreateWikiEntry([FromRoute] int CategoryID, WikiEntryCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidInputError("Invalid input", ModelState);
            }
        
            WikiEntry newWikiEntry = new WikiEntry
            {
                Title = request.Title,
                Description = request.Description,

                // Initialized with empty sections
                Stats = [],
                Sections = [],

                WikiCategoryID = CategoryID
            };

            return wikiRepository.CreateEntry(newWikiEntry);
        }

        // Get wiki entry by an ID
        [HttpGet("/Wiki/Entries/{EntryID}", Name = "GetWikiEntry")]
        public WikiEntry? GetWikiEntry(
            int EntryID,
            [FromQuery] bool includeStats = true, [FromQuery] bool includeSections = true
            )
        {
            WikiEntry? wikiEntryToGet = wikiRepository.GetEntryByID(EntryID, includeStats, includeSections);

            if (wikiEntryToGet == null)
            {
                throw new EntityNotFoundError("Wiki entry not found");
            }

            return wikiEntryToGet;
        }

        // Update a wiki entry with an ID and the new data
        [HttpPut("/Wiki/Entries/{EntryID}", Name = "UpdateWikiEntry")]
        public WikiEntry? UpdateWikiEntry(int EntryID, WikiEntryCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidInputError("Invalid input", ModelState);
            }
        
            WikiEntry? entryToUpdate = GetWikiEntry(EntryID);

            if (entryToUpdate == null)
            {
                throw new EntityNotFoundError("Wiki entry not found. Cannot be updated");
            }

            entryToUpdate.Title = request.Title;
            entryToUpdate.Description = request.Title;

            return wikiRepository.UpdateEntry(entryToUpdate);
        }

        // Delete an wiki entry by its ID
        [HttpDelete("/Wiki/Entries/{EntryID}", Name = "DeleteWikiEntry")]
        public void DeleteWikiEntry(int EntryID)
        {
            WikiEntry? entryToDelete = GetWikiEntry(EntryID);

            if (entryToDelete == null)
            {
                throw new EntityNotFoundError("Wiki entry not found. Cannot be deleted");
            }

            wikiRepository.DeleteEntry(entryToDelete);
        }
    }
}