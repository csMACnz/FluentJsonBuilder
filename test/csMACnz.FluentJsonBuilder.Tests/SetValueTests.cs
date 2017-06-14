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

    }
}