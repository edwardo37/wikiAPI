using Microsoft.AspNetCore.Mvc;
using wikiAPI.Controllers.Requests;
using wikiAPI.Exceptions;
using wikiAPI.Models;
using wikiAPI.Repositories;

namespace wikiAPI.Controllers
{
    [ApiController]
    public class WikiStatController : ControllerBase
    {
        private readonly IWikiRepository wikiRepository;

        public WikiStatController(IWikiRepository repository)
        {
            wikiRepository = repository;
        }

        [HttpPost("/Wiki/Entries/{EntryID}/Stats", Name = "CreateWikiStat")]
        public WikiStat CreateWikiStat([FromRoute] int EntryID, WikiStatCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidInputError("Invalid input", ModelState);
            }

            WikiStat newWikiStat = new WikiStat
            {
                Key = request.Key,
                Val = request.Val,
                WikiEntryID = EntryID
            };

            return wikiRepository.CreateStat(newWikiStat);
        }

        [HttpGet("/Wiki/Entries/{EntryID}/Stats", Name = "GetWikiStatsInEntry")]
        public List<WikiStat> GetWikiStatsInEntry(int EntryID)
        {
            // First, find the wiki entry by ID
            WikiEntry? wikiEntryToGet = wikiRepository.GetEntryByID(EntryID, false, false, false);

            // If the entry is not found, throw an error
            if (wikiEntryToGet == null)
            {
                throw new EntityNotFoundError("Wiki entry not found");
            }

            // If the entry is found, return its stats
            return wikiRepository.GetEntryStats(wikiEntryToGet);
        }

        [HttpPut("/Wiki/Stats/{StatID}", Name = "UpdateWikiStat")]
        public WikiStat UpdateWikiStat([FromRoute] int StatID, WikiStatCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidInputError("Invalid input", ModelState);
            }

            WikiStat? wikiStatToUpdate = wikiRepository.GetStatByID(StatID);

            if (wikiStatToUpdate == null)
            {
                throw new EntityNotFoundError("Wiki stat not found");
            }

            wikiStatToUpdate.Key = request.Key;
            wikiStatToUpdate.Val = request.Val;

            return wikiRepository.UpdateStat(wikiStatToUpdate);
        }

        [HttpDelete("/Wiki/Stats/{StatID}", Name = "DeleteWikiStat")]
        public void DeleteWikiStat([FromRoute] int StatID)
        {
            WikiStat? wikiStatToDelete = wikiRepository.GetStatByID(StatID);

            if (wikiStatToDelete == null)
            {
                throw new EntityNotFoundError("Wiki stat not found");
            }

            wikiRepository.DeleteStat(wikiStatToDelete);
        }
    }
}