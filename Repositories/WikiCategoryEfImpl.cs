using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.IdentityModel.Tokens;
using wikiAPI.Models;

namespace wikiAPI.Repositories
{
    /// <summary>
    /// Entity Framework implementation of the IWikiRepository
    /// </summary>
    public class WikiCategoryEfImpl : IWikiRepository
    {
        // Dependency injection
        private readonly WikiDbContext dbContext;
        public WikiCategoryEfImpl(WikiDbContext context)
        {
            dbContext = context;
        }


        // CATEGORIES
        public WikiCategory CreateCategory(WikiCategory category)
        {
            // Add and save changes
            dbContext.WikiCategories.Add(category);
            dbContext.SaveChanges();
            return category;
        }

        public WikiCategory? GetCategoryByID(int ID, bool includeEntries)
        {
            var query = dbContext.WikiCategories.AsQueryable();

            if (includeEntries)
            {
                query.Include(wikiCategory => wikiCategory.WikiEntries);
            }

            return query.FirstOrDefault(wikiCategory => wikiCategory.ID == ID);
        }

        public List<WikiCategory> GetCategories()
        {
            return dbContext.WikiCategories.ToList();
        }

        public WikiCategory UpdateCategory(WikiCategory category)
        {
            dbContext.SaveChanges();
            return category;
        }

        public void DeleteCategory(WikiCategory category)
        {
            dbContext.WikiCategories.Remove(category);
            dbContext.SaveChanges();
        }


        // ENTRIES
        public WikiEntry CreateEntry(WikiEntry entry)
        {
            dbContext.WikiEntries.Add(entry);
            dbContext.SaveChanges();
            return entry;
        }

        public List<WikiEntry> GetEntries()
        {
            return dbContext.WikiEntries.ToList();
        }

        public WikiEntry? GetEntryByID(int ID, bool includeStats, bool includeSections, bool includeCategoryData)
        {
            var query = dbContext.WikiEntries.AsQueryable();

            if (includeStats)
            {
                query = query.Include(wikiEntry => wikiEntry.Stats);
            }

            if (includeSections)
            {
                query = query.Include(wikiEntry => wikiEntry.Sections);
            }

            if (includeCategoryData)
            {
                query = query.Include(wikiEntry => wikiEntry.WikiCategory);
            }

            return query.FirstOrDefault(wikiEntry => wikiEntry.ID == ID);
        }

        public List<WikiEntry>? GetEntriesByCategoryID(int CategoryID)
        {
            // Includes some stats as well
            return dbContext.WikiEntries
                .Where(wikiEntry => wikiEntry.WikiCategoryID == CategoryID)
                .ToList();
        }

        public WikiEntry UpdateEntry(WikiEntry entry)
        {
            dbContext.SaveChanges();
            return entry;
        }

        public void DeleteEntry(WikiEntry entry)
        {
            dbContext.WikiEntries.Remove(entry);
            dbContext.SaveChanges();
        }
    }
}