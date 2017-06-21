using Xunit;

namespace csMACnz.FluentJsonBuilder.Tests
{
    public class SetToNullTests
    {
        [Fact]
        public void SetToNull_JsonContainsNullProperty()
        {
            string document = JsonBuilder
                .CreateObject()
                .With("property", SetTo.Null());

            Assert.Equal(@"{""property"":null}", document);
        }

        [Fact]
        public void SetToNullFunc_JsonContainsNullProperty()
        {
            string document = JsonBuilder
                .CreateObject()
                .With("property", SetTo.Null);

            Assert.Equal(@"{""property"":null}", document);
        }
    }
}