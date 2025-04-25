using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
                query = query.Include(wikiCategory => wikiCategory.WikiEntries);
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

        public WikiEntry? GetEntryByID(int ID, bool includeStats, bool includeSections)
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

            return query.FirstOrDefault(wikiEntry => wikiEntry.ID == ID);
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


        // SECTIONS

        public WikiSection CreateSection(WikiSection section)
        {
            dbContext.WikiSections.Add(section);
            dbContext.SaveChanges();
            return section;
        }

        public WikiSection? GetSectionByID(int ID)
        {
            return dbContext.WikiSections
                .FirstOrDefault(wikiSection => wikiSection.ID == ID);
        }

        public List<WikiSection> GetEntrySections(WikiEntry entry)
        {
            return dbContext.WikiSections
                .Where(wikiSection => wikiSection.WikiEntryID == entry.ID)
                .ToList();
        }

        public WikiSection UpdateSection(WikiSection section)
        {
            dbContext.SaveChanges();
            return section;
        }

        public void DeleteSection(WikiSection section)
        {
            dbContext.WikiSections.Remove(section);
            dbContext.SaveChanges();
        }


        // STATS

        public WikiStat CreateStat(WikiStat stat)
        {
            dbContext.WikiStats.Add(stat);
            dbContext.SaveChanges();
            return stat;
        }
        public WikiStat? GetStatByID(int ID)
        {
            return dbContext.WikiStats
                .FirstOrDefault(wikiStat => wikiStat.ID == ID);
        }
        public List<WikiStat> GetEntryStats(WikiEntry entry)
        {
            return dbContext.WikiStats
                .Where(wikiStat => wikiStat.WikiEntryID == entry.ID)
                .ToList();
        }

        public WikiStat UpdateStat(WikiStat stat)
        {
            dbContext.SaveChanges();
            return stat;
        }

        public void DeleteStat(WikiStat stat)
        {
            dbContext.WikiStats.Remove(stat);
            dbContext.SaveChanges();
        }
    }
}