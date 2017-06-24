using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Xunit;

namespace api.db.model.tests 
{
    public class EntryTests 
    {
         [Fact]
        public void JsonTest()
        {
            var entry = new Entry { Word = "aap", Translation = "monkey" };
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

            var json = JsonConvert.SerializeObject(entry, settings);
            
            Assert.Equal(@"{""id"":0,""word"":""aap"",""translation"":""monkey""}", json);
        }
    }
}