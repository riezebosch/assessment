using System;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace api.db.tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var options = new DbContextOptionsBuilder<DictionariesContext>()
                .UseSqlite(@"Filename=.\dictionary.db")
                .Options;
            using (var context = new DictionariesContext(options))
            {
                context.Database.EnsureCreated();

                context.Entries.Add(new Entry { Id = 0 });
                context.SaveChanges();
            }
        }
    }

    public class DictionariesContext : DbContext
    {
        public DictionariesContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Entry> Entries { get; internal set; }
    }
}
