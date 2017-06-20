using Xunit;

namespace csMACnz.FluentJsonBuilder.Tests
{
    public class WithTests
    {
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