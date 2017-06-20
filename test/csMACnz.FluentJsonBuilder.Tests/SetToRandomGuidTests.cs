using Xunit;
using System.Text.RegularExpressions;

namespace csMACnz.FluentJsonBuilder.Tests
{
    public class SetToRandomGuidTests
    {
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
}
}