using Xunit;

namespace csMACnz.FluentJsonBuilder.Tests
{
    public class SetToBooleanTests
    {
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