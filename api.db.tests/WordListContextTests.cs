using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using api.db.model;
using System.Collections.Generic;

namespace api.db.tests
{
    public class WordListContextTests
    {
        [Fact]
        public void AddNewWordListTest()
        {
            var options = new DbContextOptionsBuilder<WordListContext>()
                .UseSqlite(@"Filename=.\dictionary.db")
                .Options;
            using (var context = new WordListContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.WordLists.Add(new db.WordList
                {
                    Id = 0,
                    Title = "Dierennamen",
                    Entries = new List<Entry> 
                    {
                        new Entry { Id = 0 }
                    }
                });
                context.SaveChanges();
            }
        }
    }
}
