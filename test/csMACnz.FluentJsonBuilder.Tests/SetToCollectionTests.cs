using Xunit;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace csMACnz.FluentJsonBuilder.Tests
{
    public class SetToCollectionTests
    {
        [Fact]
        public void SetToEmptyArray_ExpectedJsonPatternReturned()
        {
            string document = JsonBuilder
                .CreateObject()
                .With("first", SetTo.EmptyArray())
                .And("second", SetTo.Value("test2"));

            Assert.Equal($@"{{""first"":[],""second"":""test2""}}", document);
        }

        [Fact]
        public void SetToEmptyArrayFunc_ExpectedJsonPatternReturned()
        {
            string document = JsonBuilder
                .CreateObject()
                .With("first", SetTo.EmptyArray)
                .And("second", SetTo.Value("test2"));

            Assert.Equal($@"{{""first"":[],""second"":""test2""}}", document);
        }
    }
}