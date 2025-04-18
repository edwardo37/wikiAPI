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

        // Create a new wiki category
        [HttpPost("", Name = "CreateWikiCategory")]
        public WikiCategory CreateWikiCategory(WikiCategoryCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidInputError("Invalid input", ModelState);
            }
            
            WikiCategory newWikiCategory = new WikiCategory
            {
                Title = request.Title,
                Description = request.Description
            };

            return wikiRepository.CreateCategory(newWikiCategory);
        }

        // Get all wiki categories
        [HttpGet("", Name = "GetWikiCategories")]
        public List<WikiCategory> GetWikiCategories()
        {
            return wikiRepository.GetCategories();
        }

        // Get a wiki category by its ID
        [HttpGet("{CategoryID}", Name = "GetWikiCategory")]
        public WikiCategory? GetWikiCategory(int CategoryID, [FromQuery] bool includeEntries = false)
        {
            WikiCategory? categoryToGet = wikiRepository.GetCategoryByID(CategoryID, includeEntries);

            if (categoryToGet == null)
            {
                throw new EntityNotFoundError("Wiki category not found");
            }

            return categoryToGet;
        }

        // Update a wiki category by its ID
        [HttpPut("{CategoryID}", Name = "UpdateWikiCategory")]
        public WikiCategory? UpdateWikiCategory(int CategoryID, WikiCategoryCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidInputError("Invalid input", ModelState);
            }

            WikiCategory? categoryToUpdate = GetWikiCategory(CategoryID);

            if (categoryToUpdate == null)
            {
                throw new EntityNotFoundError("Wiki cateogry not found. Cannot be updated");
            }

            categoryToUpdate.Title = request.Title;
            categoryToUpdate.Description = request.Description;

            return wikiRepository.UpdateCategory(categoryToUpdate);
        }

        // Delete a wiki category by its ID
        [HttpDelete("{CategoryID}", Name = "DeleteWikiCategory")]
        public void DeleteWikiCategory(int CategoryID)
        {
            WikiCategory? categoryToDelete = GetWikiCategory(CategoryID);
            
            if (categoryToDelete == null)
            {
                throw new EntityNotFoundError("Wiki category not found. Cannot be deleted");
            }

            wikiRepository.DeleteCategory(categoryToDelete);
        }
    }
}