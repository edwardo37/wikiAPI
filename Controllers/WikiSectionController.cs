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
            WikiEntry? wikiEntryToGet = wikiRepository.GetEntryByID(EntryID, false, false);

            // If the entry is not found, throw an error
            if (wikiEntryToGet == null)
            {
                throw new EntityNotFoundError("Wiki entry not found");
            }

            // If the entry is found, return its sections
            return wikiRepository.GetEntrySections(wikiEntryToGet);
        }

        [HttpPut("/Wiki/Sections/{SectionID}", Name = "UpdateWikiSection")]
        public WikiSection UpdateWikiSection([FromRoute] int SectionID, WikiSectionCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidInputError("Invalid input", ModelState);
            }

            // Find the section by ID
            WikiSection? wikiSectionToUpdate = wikiRepository.GetSectionByID(SectionID);

            // If the section is not found, throw an error
            if (wikiSectionToUpdate == null)
            {
                throw new EntityNotFoundError("Wiki section not found");
            }

            // Update the section's properties
            wikiSectionToUpdate.Header = request.Header;
            wikiSectionToUpdate.Bodies = request.Bodies;

            return wikiRepository.UpdateSection(wikiSectionToUpdate);
        }

        [HttpDelete("/Wiki/Sections/{SectionID}", Name = "DeleteWikiSection")]
        public void DeleteWikiSection([FromRoute] int SectionID)
        {
            // Find the section by ID
            WikiSection? wikiSectionToDelete = wikiRepository.GetSectionByID(SectionID);

            // If the section is not found, throw an error
            if (wikiSectionToDelete == null)
            {
                throw new EntityNotFoundError("Wiki section not found");
            }

            // Delete the section
            wikiRepository.DeleteSection(wikiSectionToDelete);
        }
    }
}