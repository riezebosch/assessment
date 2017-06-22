using System;
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
    }
}
