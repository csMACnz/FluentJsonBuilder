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
                .With("first", SetTo.AnEmptyArray())
                .And("second", SetTo.Value("test2"));

            Assert.Equal($@"{{""first"":[],""second"":""test2""}}", document);
        }

        [Fact]
        public void SetToEmptyArrayFunc_ExpectedJsonPatternReturned()
        {
            string document = JsonBuilder
                .CreateObject()
                .With("first", SetTo.AnEmptyArray)
                .And("second", SetTo.Value("test2"));

            Assert.Equal($@"{{""first"":[],""second"":""test2""}}", document);
        }


        [Fact]
        public void SetToCollectionWithOneEmptyObject_ExpectedJsonPatternReturned()
        {
            string document = JsonBuilder
                .CreateObject()
                .With("first", SetTo.AnArrayContaining(item => { }))
                .And("second", SetTo.Value("test2"));

            Assert.Equal($@"{{""first"":[{{}}],""second"":""test2""}}", document);
        }

        [Fact]
        public void SetToCollectionWithTwoComplexObjects_ExpectedJsonPatternReturned()
        {
            string document = JsonBuilder
                .CreateObject()
                .With("first", SetTo.AnArrayContaining(
                    item =>
                    {
                        item.With("first", SetTo.Value("hasValue"));
                    },
                    item =>
                    {
                        item.With("second", SetTo.Value("alsoHasAValue"));
                    }))
                .And("second", SetTo.Value("test2"));

            Assert.Equal($@"{{""first"":[{{""first"":""hasValue""}},{{""second"":""alsoHasAValue""}}],""second"":""test2""}}", document);
        }
    }
}