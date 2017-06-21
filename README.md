csMACnz.FluentJsonBuilder
================

A json string builder, built on `Newtonsoft.Json`'s JToken, using a Fluent DSL syntax, in C#.

Great for hand-crafting json strings without all the fuss of manually typing quotes, brackets and commas,
or relying on string concatination. Perfect when testing json-based web endpoints in your unit or integration tests, especially your contract validation tests (e.g. failure cases).

To use
------

1. Add the [csMACnz.FluentJsonBuilder](https://www.nuget.org/packages/csMACnz.FluentJsonBuilder/) NuGet package to your project.
1. Add some code:

``` csharp

// Create Object with 'id' set to random guid and 'name' set to value "Bob",
// and 'isAdmin set to true and 'items' set to an array containing
// item1 where item1 with 'name' set to value "Book" and 'number' set to value 4529373,
// item2 where item2 with 'name' set to value "apples" and 'quantity' set to value 12.
// Oh and 'bookingNumber' set to null

//               vvv - 'magic' implicit cast
string document = JsonBuilder
    .CreateObject()
    .With("id", SetTo.RandomGuid)
    .And("name", SetTo.Value("Bob"))
    .And("isAdmin", SetTo.True)
    .And("items", SetTo.AnArrayContaining(
        item1 => {
            item1
                .With("name", SetTo.Value("Book"))
                .And("number", SetTo.Value(4529373))
        },
        item2 => {
            item2
                .With("name", SetTo.Value("apples"))
                .And("quantity", SetTo.Value(12));
        }))
    .And("bookingNumber", SetTo.Null);
    //{"id":"FAC29444-5D24-4444-8047-304E0F8FB5F5","name":"Bob","items":[{"name":"Book","number":4529373},{"name":"apples","quantity":12},"bookingNumber":null}
```

Alternatively, extend for a more DSL approach:

``` csharp
public class MyComplexObject : JsonObjectBuilder<MyComplexObject>
{
    public static MyComplexObject Create()
    {
        return new MyComplexObject();
    }

    internal MyComplexObject WithId(Modifier modifier)
    {
        return With("id", modifier);
    }

    internal MyComplexObject WithName(Modifier modifier)
    {
        return With("name", modifier);
    }

    internal MyComplexObject WithItems(params Action<ItemObject>[] initialisers)
    {
        return With("items", SetTo.AnArrayContaining(initialisers));
    }

    internal MyComplexObject UpdateExistingTagAtIndex(int index, Action<ItemObject> update)
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

string document = MyComplexObject
    .Create()
    .WithId(SetTo.Value("ID123"))
    .WithItems(
        item0 => item0
            .WithId(SetTo.Value("ARandomGuid"))
            .WithTags(
                tag0 => tag0
                    .WithKey(SetTo.Value("Importance"))
                    .WithValue(SetTo.Value("High")),
                tag1 => tag1
                    .WithKey(SetTo.Value("Level"))
                    .WithValue(SetTo.Null),
                tag2 => tag2
                    .WithKey(SetTo.Value("Games"))
                    .WithValue(SetTo.Null)))
    .WithName(SetTo.Value("Test"))
    .WithId(Updated.By<string>(v => v + "_suffix"))
    .UpdateExistingTagAtIndex(0,
        item0 => item0
            .UpdateExistingTagAtIndex(1, tag => tag.WithValue(SetTo.Value("Medium")))
            .WithTags(Updated.ByRemovingAtIndex(0)));

//{"id":"ID123_suffix","items":[{"id":"ARandomGuid","tags":[{"key":"Level","value":"Medium"},{"key":"Games","value":null}]}],"name":"Test"}

```

The Methods
-----------

each builder comes with the methods

* `With(string propertyName)` - includes the named property in the result json object, defaulting to `null`
* `With(string propertyName, Modifier modifier)` - apply a modifier to the named property (see below)
* `And(string propertyName, Modifier modifier)` - an alias for `With` (for grammatical reasons)
* `Without(string propertyName)` - removes the property from appearing in the result json object

and Modifiers:

* `SetTo.Null` - set a property to `null`
* `SetTo.RandomGuid` - Generates a Random Guid (UPPER-CASE) for a property
* `SetTo.True` - set a property to `true`
* `SetTo.False` - set a property to `false`
* `SetTo.AnEmptyArray` - set a property to `[]`
* `SetTo.Value(JToken value)` - set a property to a specified value (leveraging `Newtonsoft.Json`'s `JToken` implicit casting to support many values)
* `SetTo.AnArrayContaining(params Action<JsonObjectBuilder>[] setValues)` - define nested items in a collection for an array property
* `Updated.AtIndex(int index, Action<JsonObjectBuilder> update)` - update an item in an array property based on its index
* `Updated.ByRemovingAtIndex(int index)` - remove an item in an array property based on its index
* `Updated.By(Func<JToken, JToken> update)` - custom update lambda, accessing the existing raw JToken value (if one exists).
* `Updated.By<T>(Func<T, JToken> update)` - custom update lambda, leveraging `JToken`'s `.Value<T>()` method to typecasting the existing value

Note that there is no reflection or retrospection for update actions. The assumption is that you are in a context where you understand the state of your builder, and know what the types and values are when performing updates. If in doubt replace the value using a `SetTo` modifier.


Bugs, Edge-cases and Extensions
-------------------------------

Having troubles? Got a feature request? feel the grammer doesn't fit your edge-case? Raise and issue or open a pull request (With Tests) to explain the concern and I'll take a look!