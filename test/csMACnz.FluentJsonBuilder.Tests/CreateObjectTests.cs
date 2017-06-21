using Xunit;

namespace csMACnz.FluentJsonBuilder.Tests
{
    public class CreateObjectTests
    {
        [Fact]
        public void ImplicitCastInAssertWorksCorrectly()
        {
            var document = JsonBuilder.CreateObject();
            Assert.Equal("{}", document);
        }

        [Fact]
        public void ImplicitCastOnAssignmentWorksCorrectly()
        {
            string document = JsonBuilder.CreateObject();
            Assert.Equal("{}", document);
        }
    }
}