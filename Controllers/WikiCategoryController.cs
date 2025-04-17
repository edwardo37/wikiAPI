using Microsoft.AspNetCore.Mvc;
using wikiAPI.Controllers.Requests;
using wikiAPI.Exceptions;
using wikiAPI.Models;
using wikiAPI.Repositories;

namespace wikiAPI.Controllers
{
    [Route("/Wiki/Categories")]
    [ApiController]
    public class WikiCategoryController : ControllerBase
    {
        private readonly IWikiRepository wikiRepository;

        public WikiCategoryController(IWikiRepository repository)
        {
            wikiRepository = repository;
        }

        [HttpPost("", Name = "CreateWikiCategory")]
        public WikiCategory CreateWikiCategory(WikiCategoryCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidInputError("Invalid input", ModelState);
            }
            else
            {
                WikiCategory newWikiCategory = new WikiCategory
                {
                    Title = request.Title,
                    Description = request.Description
                };

                return wikiRepository.CreateCategory(newWikiCategory);
            }
        }

        [HttpGet("", Name = "GetWikiCategories")]
        public List<WikiCategory> GetWikiCategories()
        {
            return wikiRepository.GetCategories();
        }

        [HttpGet("{CategoryID}", Name = "GetWikiCategory")]
        public WikiCategory? GetWikiCategory(int CategoryID, [FromQuery] bool includeEntries = false)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidInputError("Invalid input", ModelState);
            }

            WikiCategory? categoryToGet = wikiRepository.GetCategoryByID(CategoryID, includeEntries);

            if (categoryToGet == null)
            {
                throw new EntityNotFoundError("Wiki category not found");
            }

            return categoryToGet;
        }

        [HttpPut("{CategoryID}", Name = "UpdateWikiCategory")]
        public WikiCategory? UpdateWikiCategory(int CategoryID, WikiCategoryCreateRequest request)
        {
            WikiCategory? categoryToUpdate = GetWikiCategory(CategoryID);

            if (!ModelState.IsValid)
            {
                throw new InvalidInputError("Invalid input", ModelState);
            }
            else
            {
                if (categoryToUpdate == null)
                {
                    throw new EntityNotFoundError("Wiki cateogry not found. Cannot be updated");
                }

                categoryToUpdate.Title = request.Title;
                categoryToUpdate.Description = request.Description;

                return wikiRepository.UpdateCategory(categoryToUpdate);
            }
        }

        [HttpDelete("{CategoryID}", Name = "DeleteWikiCategory")]
        public void DeleteWikiCategory(int CategoryID)
        {
            WikiCategory? categoryToDelete = GetWikiCategory(CategoryID);

            if (!ModelState.IsValid)
            {
                throw new InvalidInputError("Invalid input", ModelState);
            }
            else
            {
                if (categoryToDelete == null)
                {
                    throw new EntityNotFoundError("Wiki category not found. Cannot be deleted");
                }

                wikiRepository.DeleteCategory(categoryToDelete);
            }
        }
    }
}