using Microsoft.EntityFrameworkCore;
using wikiAPI.Models;

namespace wikiAPI.Repositories
{
    public class WikiDbContext : DbContext
    {
        public WikiDbContext(DbContextOptions<WikiDbContext> options) : base(options) { }

        public DbSet<WikiCategory> WikiCategories { get; set; }
        public DbSet<WikiEntry> WikiEntries { get; set; }
        public DbSet<WikiSection> WikiSections { get; set; }
        public DbSet<WikiStat> WikiStats { get; set; }
    }
}