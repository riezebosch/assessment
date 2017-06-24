using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using api.db.model;

namespace api.db.tests
{
    public class WordListContextTests
    {
        [Fact]
        public void AddNewEntryTest()
        {
            var options = new DbContextOptionsBuilder<WordListContext>()
                .UseSqlite(@"Filename=.\dictionary.db")
                .Options;
            using (var context = new WordListContext(options))
            {
                context.Database.EnsureCreated();

                context.Entries.Add(new Entry { Id = 0 });
                context.SaveChanges();
            }
        }
    }
}
