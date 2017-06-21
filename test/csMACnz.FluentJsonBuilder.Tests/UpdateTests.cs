using Xunit;

namespace csMACnz.FluentJsonBuilder.Tests
{
    public class UpdateTests
    {
        [Fact]
        public void UpdateAllowsToModifyExistingSetValue()
        {
            string document = JsonBuilder
                .CreateObject()
                .With("first", SetTo.Value("test1"))
                .With("first", Updated.By(v => v + " and test2"))
                .And("second", SetTo.Value(true));

            Assert.Equal(@"{""first"":""test1 and test2"",""second"":true}", document);
        }

        [Fact]
        public void UpdateAllowsToModifyExistingSetValueUsingStrongTyping()
        {
            string document = JsonBuilder
                .CreateObject()
                .With("first", SetTo.Value("test1"))
                .And("second", SetTo.Value(true))
                .And("second", Updated.By<bool>(v => !v));

            Assert.Equal(@"{""first"":""test1"",""second"":false}", document);
        }
    }
}