using System;
using Newtonsoft.Json.Linq;

namespace csMACnz.FluentJsonBuilder
{
    public static class Updated
    {
        public static Modifier AtIndex(int index, Action<JsonObjectBuilder> update)
        {
            return AtIndex<JsonObjectBuilder>(index, update);
        }

        public static Modifier AtIndex<TItemBuilder>(
            int index,
            Action<TItemBuilder> update)
            where TItemBuilder : JsonObjectBuilder<TItemBuilder>, new()
        {
            return new Modifier(
                array =>
                {
                    JObject item = (JObject)((JArray)array)[index];
                    var builder = new TItemBuilder();
                    builder.Rebase(item);
                    update(builder);
                    return array;
                });
        }

        public static Modifier ByRemovingAtIndex(int index)
        {
            return new Modifier(
                token =>
                {
                    var array = (JArray)token;
                    array.RemoveAt(index);
                    return array;
                });
        }

        public static Modifier By(Func<JToken, JToken> update)
        {
            return new Modifier(update);
        }

        public static Modifier By<T>(Func<T, JToken> update)
        {
            return new Modifier(v => update(v.Value<T>()));
        }
    }
}
