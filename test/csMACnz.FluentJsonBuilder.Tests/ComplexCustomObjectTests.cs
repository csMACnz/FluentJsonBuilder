using Xunit;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System;

namespace csMACnz.FluentJsonBuilder.Tests
{
    public class ComplexCustomObjectTests
    {
        [Fact]
        public void CanCreateComplexObject()
        {
            string document = ComplexObject.Create();
            Assert.Equal("{}", document);
        }

        [Fact]
        public void CanSetupEntireTreeStructure()
        {
            string document = ComplexObject
            .Create()
            .WithId(SetTo.Value("ID123"))
            .WithTags(
                tag1 => tag1
                    .WithKey(SetTo.Value("Importance"))
                    .WithValue(SetTo.Value("High")),
                tag2 => tag2
                    .WithKey(SetTo.Value("Level"))
                    .WithValue(SetTo.Value("Medium")),
                tag3 => tag3
                    .WithKey(SetTo.Value("Games"))
                    .WithValue(SetTo.Null))
            .WithName(SetTo.Value("Test"));

            Assert.Equal(@"{""id"":""ID123"",""tags"":[{""key"":""Importance"",""value"":""High""},{""key"":""Level"",""value"":""Medium""},{""key"":""Games"",""value"":null}],""name"":""Test""}", document);
        }

        private class ComplexObject : JsonObjectBuilder<ComplexObject>
        {
            public static ComplexObject Create()
            {
                return new ComplexObject();
            }

            internal ComplexObject WithId(SetTo setTo)
            {
                return With("id", setTo);
            }

            internal ComplexObject WithName(SetTo setTo)
            {
                return With("name", setTo);
            }

            internal ComplexObject WithTags(params Action<TagObject>[] initialisers)
            {
                return With("tags", SetTo.AnArrayContaining<TagObject>(initialisers));
            }
        }

        private class TagObject : JsonObjectBuilder<TagObject>
        {
            internal TagObject WithKey(SetTo setTo)
            {
                return With("key", setTo);
            }
            internal TagObject WithValue(SetTo setTo)
            {
                return With("value", setTo);
            }
        }
    }
}