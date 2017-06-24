using System;
using System.IO;
using System.Net;
using api.db;
using api.db.model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
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

        [Fact]
        public async void IntegrationTest()
        {
            using (var server = StartTestServer())
            using (var client = server.CreateClient())
            {
                var result = await client.GetAsync("api/Values");
                Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            }
        }

        private static TestServer StartTestServer()
        {
            return new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
        }
    }
}
