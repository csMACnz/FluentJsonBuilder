using System;
using Newtonsoft.Json.Linq;

namespace csMACnz.FluentJsonBuilder
{
    public static class SetTo
    {
        public static Modifier.ModifierFunc Null => () => new Modifier(_ => null);

        public static Modifier.ModifierFunc RandomGuid => () =>
        {
            return new Modifier(_ => Guid.NewGuid().ToString().ToUpper());
        };

        public static Modifier.ModifierFunc True => () => { return new Modifier(_ => true); };

        public static Modifier.ModifierFunc False => () => { return new Modifier(_ => false); };

        public static Modifier.ModifierFunc AnEmptyArray => () => { return new Modifier(_ => new JArray()); };

        public static Modifier Value(JToken value)
        {
            return new Modifier(_ => value);
        }

        public static Modifier AnArrayContaining(params Action<JsonObjectBuilder>[] setValues)
        {
            return AnArrayContaining<JsonObjectBuilder>(setValues);
        }

        public static Modifier AnArrayContaining<TItemBuilder>(params Action<TItemBuilder>[] setValues)
            where TItemBuilder : JsonObjectBuilder<TItemBuilder>, new()
        {
            return new Modifier(_ =>
            {
                var jarray = new JArray();
                foreach (var updateAction in setValues)
                {
                    var itemBuilder = new TItemBuilder();
                    updateAction(itemBuilder);
                    jarray.Add(itemBuilder.GetObject());
                }
                return jarray;
            });
        }
    }
}