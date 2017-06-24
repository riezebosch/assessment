using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using api.db;
using api.db.model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.tests
{
    public class ValuesControllerTests
    {
        [Fact()]
        public async void TestGetWordLists()
        {
            using (var context = CreateContext())
            using (var sut = new ValuesController(context))
            {
                var response = Assert.IsType<OkObjectResult>(await sut.Get());
                var result = Assert.IsAssignableFrom<IEnumerable<WordList>>(response.Value);
                var list = result.Single();

                Assert.Equal("Dierennamen", list.Title);
            }
        }

        [Fact()]
        public async void Test1()
        {
            using (var context = CreateContext())
            using (var sut = new ValuesController(context))
            {
                var response = Assert.IsType<OkObjectResult>(await sut.Get(1));
                var result = Assert.IsAssignableFrom<WordList>(response.Value);

                Assert.Equal(new[] { new Entry { Word = "aap", Translation = "monkey", Id = 1 } }, result.Entries, new EntryComparer());
            }
        }

        [Fact()]
        public async void EntriesNotExistingWordList()
        {
            using (var context = CreateContext())
            using (var sut = new ValuesController(context))
            {
                var response = Assert.IsType<NotFoundObjectResult>(await sut.Get(1234));
                Assert.Equal(1234, response.Value);
            }
        }
        private static WordListContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<WordListContext>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .Options;

            var context = new WordListContext(options);
            var entry = new Entry
            {
                Word = "aap",
                Translation = "monkey"
            };

            context.WordLists.Add(new WordList
            {
                Title = "Dierennamen",
                Entries = new List<Entry> { entry }
            });
            context.SaveChanges();

            return context;
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
