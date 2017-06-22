using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Xunit;

namespace api.Controllers.tests
{
    public class ValuesControllerTests
    {
        [Fact]
        public void Test1()
        {
            var controller = new ValuesController();
            Assert.Equal(new []{"value1", "value2"}, controller.Get());
        }

        [Fact]
        public void JsonTest()
        {
            var entry = new Entry { Word = "aap", Translation = "monkey" };
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

            var json = JsonConvert.SerializeObject(entry, settings);
            
            Assert.Equal(@"{""word"":""aap"",""translation"":""monkey""}", json);
        }

        private class Entry
        {
            public string Word { get; set; }
            public string Translation { get; set; }
        }
    }
}
