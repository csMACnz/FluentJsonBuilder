using Xunit;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace csMACnz.FluentJsonBuilder.Tests
{
    public class SetToTests
    {
        [Fact]
        public void SettingEmptyStringProperty_JsonContainsEmptyPropertyName()
        {
            string document = JsonBuilder
                .CreateObject()
                .With("", SetTo.Value(""));

            Assert.Equal(@"{"""":""""}", document);
        }

        [Fact]
        public void SetToNullFunc_JsonContainsNullProperty()
        {
            string document = JsonBuilder
                .CreateObject()
                .With("property", SetTo.Null);

            Assert.Equal(@"{""property"":null}", document);
        }

        [Fact]
        public void SetToNull_JsonContainsNullProperty()
        {
            string document = JsonBuilder
                .CreateObject()
                .With("property", SetTo.Null());

            Assert.Equal(@"{""property"":null}", document);
        }

        [Fact]
        public void AddingEmptyProperties_PropertiesAppearInJson()
        {
            string document = JsonBuilder
                .CreateObject()
                .With("first")
                .With("second")
                .With("third");

            Assert.Equal(@"{""first"":null,""second"":null,""third"":null}", document);
        }

        [Fact]
        public void AddingPropertiesWithValues_PropertiesAppearInJson()
        {
            string document = JsonBuilder
                .CreateObject()
                .With("first", SetTo.Value("test1"))
                .And("second", SetTo.Value("test2"))
                .With("third", SetTo.Value(true));

            Assert.Equal(@"{""first"":""test1"",""second"":""test2"",""third"":true}", document);
        }

        [Fact]
        public void AddingPropertiesUsingWithAndAnd_PropertiesAppearInJson()
        {
            string document = JsonBuilder
                .CreateObject()
                .With("first", SetTo.Value("test1"))
                .And("second", SetTo.Value("test2"))
                .With("third", SetTo.Null)
                .And("fourth", SetTo.Null)
                .With("fifth", SetTo.Null())
                .And("sixth", SetTo.Null())
                .With("seventh")
                .And("eighth");

            Assert.Equal(@"{""first"":""test1"",""second"":""test2"",""third"":null,""fourth"":null,""fifth"":null,""sixth"":null,""seventh"":null,""eighth"":null}", document);
        }

        [Fact]
        public void AddingPropertiesUsingSetToRandomGuid_ExpectedJsonPatternReturned()
        {
            string document = JsonBuilder
                .CreateObject()
                .With("first", SetTo.RandomGuid())
                .And("second", SetTo.Value("test2"));

            var guidRegex = @"[0-9A-F]{8}-([0-9A-F]{4}-){3}[0-9A-F]{12}";
            Assert.True(Regex.IsMatch(document, $@"{{""first"":""{guidRegex}"",""second"":""test2""}}"));
        }

        [Fact]
        public void AddingPropertiesUsingSetToRandomGuidFunction_ExpectedJsonPatternReturned()
        {
            string document = JsonBuilder
                .CreateObject()
                .With("first", SetTo.RandomGuid)
                .And("second", SetTo.Value("test2"));

            var guidRegex = @"[0-9A-F]{8}-([0-9A-F]{4}-){3}[0-9A-F]{12}";
            Assert.True(Regex.IsMatch(document, $@"{{""first"":""{guidRegex}"",""second"":""test2""}}"));
        }

        [Fact]
        public void AddingPropertiesUsingSetToTrue_ExpectedJsonPatternReturned()
        {
            string document = JsonBuilder
                .CreateObject()
                .With("first", SetTo.True())
                .And("second", SetTo.Value("test2"));

            Assert.Equal($@"{{""first"":true,""second"":""test2""}}", document);
        }

        [Fact]
        public void AddingPropertiesUsingSetToTrueFunction_ExpectedJsonPatternReturned()
        {
            string document = JsonBuilder
                .CreateObject()
                .With("first", SetTo.True)
                .And("second", SetTo.Value("test2"));

            Assert.Equal($@"{{""first"":true,""second"":""test2""}}", document);
        }

        [Fact]
        public void AddingPropertiesUsingSetToFalse_ExpectedJsonPatternReturned()
        {
            string document = JsonBuilder
                .CreateObject()
                .With("first", SetTo.False())
                .And("second", SetTo.Value("test2"));

            Assert.Equal($@"{{""first"":false,""second"":""test2""}}", document);
        }

        [Fact]
        public void AddingPropertiesUsingSetToFalseFunction_ExpectedJsonPatternReturned()
        {
            string document = JsonBuilder
                .CreateObject()
                .With("first", SetTo.False)
                .And("second", SetTo.Value("test2"));

            Assert.Equal($@"{{""first"":false,""second"":""test2""}}", document);
        }
    }
}