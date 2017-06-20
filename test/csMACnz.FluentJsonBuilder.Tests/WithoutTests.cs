using Xunit;

namespace csMACnz.FluentJsonBuilder.Tests
{
    public class WithoutTests
    {
        [Fact]
        public void WithThenWithout_PropertyDoesNotAppearInJson()
        {
            string document = JsonBuilder
                .CreateObject()
                .With("first")
                .With("second")
                .Without("first");

            Assert.Equal(@"{""second"":null}", document);
        }
        [Fact]
        public void WithoutThenWith_PropertyAppearsInJson()
        {
            string document = JsonBuilder
                .CreateObject()            
                .Without("first")
                .With("first", SetTo.Value("There"));

            Assert.Equal(@"{""first"":""There""}", document);
        }
        [Fact]
        public void WithThenWithoutThenWith_PropertyAppearsInJson()
        {
            string document = JsonBuilder
                .CreateObject()
                .With("first", SetTo.Value("NotThere"))
                .With("second")
                .Without("first")
                .With("first");

            Assert.Equal(@"{""second"":null,""first"":null}", document);
        }
    }
}