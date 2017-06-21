using Xunit;

namespace csMACnz.FluentJsonBuilder.Tests
{
    public class SetToValueTests
    {
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
        public void SettingEmptyStringProperty_JsonContainsEmptyPropertyName()
        {
            string document = JsonBuilder
                .CreateObject()
                .With("", SetTo.Value(""));

            Assert.Equal(@"{"""":""""}", document);
        }
    }
}