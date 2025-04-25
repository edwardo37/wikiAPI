using wikiAPI.Models;

namespace wikiAPI.Repositories
{
    /// <summary>
    /// Interface for all Wiki repositories
    /// </summary>
    /// <author> Cameron Schultz </author>
    public interface IWikiRepository
    {
        // I didn't know if the interface has ALL methods for ALL entities, or if there should be multiple interfaces, so I chose all

        // CATEGORIES

        /// <summary>
        /// Save a new category
        /// </summary>
        /// <param name="category">The category to save</param>
        /// <returns>The category saved</returns>
        WikiCategory CreateCategory(WikiCategory category);

        /// <summary>
        /// Get a specific category
        /// </summary>
        /// <param name="ID">The ID of the category to request</param>
        /// <param name="includeEntries">Whether to include all entries in the category</param>
        /// <returns>The category requested, null if not found</returns>
        WikiCategory? GetCategoryByID(int ID, bool includeEntries);

        /// <summary>
        /// Get a list of all categories
        /// </summary>
        /// <returns>A list of all categories</returns>
        List<WikiCategory> GetCategories();

        /// <summary>
        /// Update a specific category
        /// </summary>
        /// <param name="category">The category to update</param>
        /// <returns></returns>
        WikiCategory UpdateCategory(WikiCategory category);

        /// <summary>
        /// Delete a specific category
        /// </summary>
        /// <param name="category">The category to delete</param>
        void DeleteCategory(WikiCategory category);


        // ENTRIES

        /// <summary>
        /// Save a new entry
        /// </summary>
        /// <param name="entry">The entry to save</param>
        /// <returns>The newly saved entry</returns>
        WikiEntry CreateEntry(WikiEntry entry);

        /// <summary>
        /// Get all wiki entries
        /// </summary>
        /// <returns>A list of all wiki entries</returns>
        List<WikiEntry> GetEntries();

        /// <summary>
        /// Get an entry
        /// </summary>
        /// <param name="ID">The ID of entry to fetch</param>
        /// <param name="includeStats">Wether to include info about the entry's stats</param>
        /// <param name="includeSections">Wether to include info about the entry's sections</param>
        /// <param name="includeCategoryData">Wether to include info about the containing category</param>
        /// <returns>The entry, null if not found</returns>
        WikiEntry? GetEntryByID(int ID, bool includeStats, bool includeSections, bool includeCategoryData);

        /// <summary>
        /// Update an entry
        /// </summary>
        /// <param name="entry">The entry to update</param>
        /// <returns>The newly updated entry</returns>
        WikiEntry UpdateEntry(WikiEntry entry);

        /// <summary>
        /// Delete an entry
        /// </summary>
        /// <param name="entry">The entry to delete</param>
        void DeleteEntry(WikiEntry entry);


        // SECTIONS

        /// <summary>
        /// Create a new section
        /// </summary>
        /// <param name="Section">The section to register</param>
        /// <returns>The newly created section</returns>
        WikiSection CreateSection(WikiSection Section);

        /// <summary>
        /// Get a section by its ID
        /// </summary>
        /// <param name="ID">The ID of the Section to Get</param>
        /// <returns>The section -- null if nonexistant</returns>
        WikiSection? GetSectionByID(int ID);

        /// <summary>
        /// Get all sections in an entry
        /// </summary>
        /// <param name="entry"></param>
        /// <returns>A list of stats</returns>
        List<WikiSection> GetEntrySections(WikiEntry entry);

        /// <summary>
        /// Update a section
        /// </summary>
        /// <param name="section">The section to update</param>
        /// <returns>The section updated</returns>
        WikiSection UpdateSection(WikiSection section);

        /// <summary>
        /// Delete a section
        /// </summary>
        /// <param name="section">The section to delete</param>
        void DeleteSection(WikiSection section);


        // STATS

        /// <summary>
        /// Create a new stat
        /// </summary>
        /// <param name="stat">The stat to create</param>
        /// <returns>The newly created stat</returns>
        WikiStat CreateStat(WikiStat stat);

        /// <summary>
        /// Get a stat by its ID
        /// </summary>
        /// <param name="ID">The ID of the of the stat to get</param>
        /// <returns>The stat fetched</returns>
        WikiStat? GetStatByID(int ID);

        /// <summary>
        /// Get all stats in an entry
        /// </summary>
        /// <param name="entry">The entry to list stats from</param>
        /// <returns>A list of stats</returns>
        List<WikiStat> GetEntryStats(WikiEntry entry);

        /// <summary>
        /// Update a stat
        /// </summary>
        /// <param name="stat">Stat to update</param>
        /// <returns>Updated stat</returns>
        WikiStat UpdateStat(WikiStat stat);

        /// <summary>
        /// Delete a stat
        /// </summary>
        /// <param name="stat">Stat to delete</param>
        void DeleteStat(WikiStat stat);
    }
}