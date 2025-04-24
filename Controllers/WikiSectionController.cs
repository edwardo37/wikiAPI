using Microsoft.AspNetCore.Mvc;
using wikiAPI.Controllers.Requests;
using wikiAPI.Exceptions;
using wikiAPI.Models;
using wikiAPI.Repositories;

namespace wikiAPI.Controllers
{
    [ApiController]
    public class WikiSectionController : ControllerBase
    {
        private readonly IWikiRepository wikiRepository;

        public WikiSectionController(IWikiRepository repository)
        {
            wikiRepository = repository;
        }

        [HttpPost("/Wiki/Entries/{EntryID}/Sections", Name = "CreateWikiSection")]
        public WikiSection CreateWikiSection([FromRoute] int EntryID, WikiSectionCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidInputError("Invalid input", ModelState);
            }

            WikiSection newWikiSection = new WikiSection
            {
                Header = request.Header,
                Bodies = request.Bodies,
                WikiEntryID = EntryID
            };

            return wikiRepository.CreateSection(newWikiSection);
        }

        [HttpGet("/Wiki/Entries/{EntryID}/Sections", Name = "GetWikiSectionsInEntry")]
        public List<WikiSection> GetWikiSectionsInEntry(int EntryID)
        {
            // First, find the wiki entry by ID
            WikiEntry? wikiEntryToGet = wikiRepository.GetEntryByID(EntryID, false, false, false);

            // If the entry is not found, throw an error
            if (wikiEntryToGet == null)
            {
                throw new EntityNotFoundError("Wiki entry not found");
            }

            // If the entry is found, return its sections
            return wikiRepository.GetEntrySections(wikiEntryToGet);
        }
    }
}