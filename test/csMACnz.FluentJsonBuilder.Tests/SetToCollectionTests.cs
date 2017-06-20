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

        [Fact]
        public void SetToCollectionThenUpdated_ExpectedJsonPatternReturned()
        {
            string document = JsonBuilder
                .CreateObject()
                .With("first", SetTo.AnArrayContaining(item => { },item => { }))
                .And("second", SetTo.Value("test2"))
                .With("first", Updated.AtIndex(0,
                    item => item.With("first", SetTo.Value("hasValue"))))
                .With("first", Updated.AtIndex(1,
                    item => item.With("second", SetTo.Value("alsoHasAValue"))));

            Assert.Equal($@"{{""first"":[{{""first"":""hasValue""}},{{""second"":""alsoHasAValue""}}],""second"":""test2""}}", document);
        }

        
        [Fact]
        public void SetToCollectionThenUpdatedByRemoving_ExpectedJsonPatternReturned()
        {
            string document = JsonBuilder
                .CreateObject()
                .With("first", SetTo.AnArrayContaining(
                    item => item.With("aProperty", SetTo.Value("AValue")),
                    item => { }))
                .And("second", SetTo.Value("test2"))
                .With("first", Updated.ByRemovingAtIndex(0))
                .With("first", Updated.AtIndex(0,
                    item => item.With("second", SetTo.Value("NewValue"))));

            Assert.Equal($@"{{""first"":[{{""second"":""NewValue""}}],""second"":""test2""}}", document);
        }
    }
}