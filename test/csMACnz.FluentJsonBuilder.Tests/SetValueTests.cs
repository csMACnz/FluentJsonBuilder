using Xunit;
using Newtonsoft.Json;

namespace csMACnz.FluentJsonBuilder.Tests
{
    public class SetValueTests
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
    }
}