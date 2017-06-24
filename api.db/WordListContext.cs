using Microsoft.EntityFrameworkCore;
using api.db.model;

namespace api.db
{
    public class WordListContext : DbContext
    {
        public WordListContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Entry> Entries { get; internal set; }
    }
}