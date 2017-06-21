using Xunit;

namespace csMACnz.FluentJsonBuilder.Tests
{
    public class SimpleCustomObjectTests
    {
        private class CustomObject : JsonObjectBuilder<CustomObject>
        {
            public static CustomObject Create()
            {
                return new CustomObject();
            }

            internal CustomObject WithCat(Modifier modifer)
            {
                return With("cat", modifer);
            }

            internal CustomObject WithId(Modifier modifier)
            {
                return With("id", modifier);
            }

            internal CustomObject WithName(Modifier modifier)
            {
                return With("name", modifier);
            }
        }

        [Fact]
        public void CanCreateCustomObject()
        {
            string document = CustomObject.Create();
            Assert.Equal("{}", document);
        }

        [Fact]
        public void CanSetCustomPropertiesAndGenericProperties()
        {
            string document = CustomObject
                .Create()
                .WithId(SetTo.Value("ID123"))
                .With("description", SetTo.Value("This is a description"))
                .WithName(SetTo.Value("Test"))
                .With("Age", SetTo.Value(42))
                .WithCat(SetTo.Null);

            Assert.Equal(
                @"{""id"":""ID123"",""description"":""This is a description"",""name"":""Test"",""Age"":42,""cat"":null}",
                document);
        }
    }
}