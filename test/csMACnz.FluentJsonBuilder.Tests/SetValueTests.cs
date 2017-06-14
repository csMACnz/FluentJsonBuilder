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

    }
}