using System;
using Newtonsoft.Json.Linq;
using Xunit;

namespace csMACnz.FluentJsonBuilder.Tests
{
    public class ComplexCustomObjectTests
    {
        private class ComplexObject : JsonObjectBuilder<ComplexObject>
        {
            public static ComplexObject Create()
            {
                return new ComplexObject();
            }

            internal ComplexObject WithId(Modifier modifier)
            {
                return With("id", modifier);
            }

            internal ComplexObject WithName(Modifier modifier)
            {
                return With("name", modifier);
            }

            internal ComplexObject WithItems(params Action<ItemObject>[] initialisers)
            {
                return With("items", SetTo.AnArrayContaining(initialisers));
            }

            internal ComplexObject UpdateExistingTagAtIndex(int index, Action<ItemObject> update)
            {
                return With("items", Updated.AtIndex(index, update));
            }
        }

        private class ItemObject : JsonObjectBuilder<ItemObject>
        {
            internal ItemObject WithId(Modifier modifier)
            {
                return With("id", modifier);
            }

            internal ItemObject WithName(Modifier modifier)
            {
                return With("name", modifier);
            }

            internal ItemObject WithTags(params Action<TagObject>[] initialisers)
            {
                return With("tags", SetTo.AnArrayContaining(initialisers));
            }

            internal ItemObject WithTags(Modifier modifier)
            {
                return With("tags", modifier);
            }

            internal ItemObject UpdateExistingTagAtIndex(int index, Action<TagObject> update)
            {
                return With("tags", Updated.AtIndex(index, update));
            }
        }

        private class TagObject : JsonObjectBuilder<TagObject>
        {
            internal TagObject WithKey(Modifier modifier)
            {
                return With("key", modifier);
            }

            internal TagObject WithValue(Modifier modifier)
            {
                return With("value", modifier);
            }
        }

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
                .WithItems(
                    item => item
                        .WithId(SetTo.Value("ARandomGuid"))
                        .WithTags(
                            tag1 => tag1
                                .WithKey(SetTo.Value("Importance"))
                                .WithValue(SetTo.Value("High")),
                            tag2 => tag2
                                .WithKey(SetTo.Value("Level"))
                                .WithValue(SetTo.Value("Medium")),
                            tag3 => tag3
                                .WithKey(SetTo.Value("Games"))
                                .WithValue(SetTo.Null)))
                .WithName(SetTo.Value("Test"));

            Assert.Equal(
                @"{""id"":""ID123"",""items"":[{""id"":""ARandomGuid"",""tags"":[{""key"":""Importance"",""value"":""High""},{""key"":""Level"",""value"":""Medium""},{""key"":""Games"",""value"":null}]}],""name"":""Test""}",
                document);
        }

        [Fact]
        public void CanSetupTreeStructureWithUpdates()
        {
            string document = ComplexObject
                .Create()
                .WithId(SetTo.Value("ID123"))
                .WithItems(
                    item => item
                        .WithId(SetTo.Value("ARandomGuid"))
                        .WithTags(
                            tag1 => tag1
                                .WithKey(SetTo.Value("Importance"))
                                .WithValue(SetTo.Value("High")),
                            tag2 => tag2
                                .WithKey(SetTo.Value("Level"))
                                .WithValue(SetTo.Null),
                            tag3 => tag3
                                .WithKey(SetTo.Value("Games"))
                                .WithValue(SetTo.Null)))
                .WithName(SetTo.Value("Test"))
                .UpdateExistingTagAtIndex(0,
                    item => item
                        .UpdateExistingTagAtIndex(1, tag => tag.WithValue(SetTo.Value("Medium")))
                        .WithTags(Updated.ByRemovingAtIndex(0)));

            Assert.Equal(
                @"{""id"":""ID123"",""items"":[{""id"":""ARandomGuid"",""tags"":[{""key"":""Level"",""value"":""Medium""},{""key"":""Games"",""value"":null}]}],""name"":""Test""}",
                document);
        }
    }
}