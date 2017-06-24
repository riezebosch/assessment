using System;
using api.db;
using api.db.model;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace api.Controllers.tests
{
    public class ValuesControllerTests
    {
        [Fact()]
        public void Test1()
        {
            var options = new DbContextOptionsBuilder<WordListContext>()
                .UseInMemoryDatabase()
                .Options;
                
            using (var context = new WordListContext(options))
            using(var controller = new ValuesController(context))
            {
                Entry entry = new Entry
                {
                    Word = "aap",
                    Translation = "monkey"
                };
                context.Entries.Add(entry);
                context.SaveChanges();

                Assert.Equal(new[] { entry }, controller.Get(), new EntryComparer());
            }
        }
    }
}
