using System;
using Newtonsoft.Json.Linq;

namespace csMACnz.FluentJsonBuilder
{
    public class Updated
    {
        Func<JToken, JToken> _function;

        private Updated(Func<JToken, JToken> function)
        {
            _function = function;
        }

        public static Updated AtIndex(int index, Action<JsonObjectBuilder> update)
        {
            return AtIndex<JsonObjectBuilder>(index, update);
        }

        public static Updated AtIndex<TItemBuilder>(
            int index,
            Action<TItemBuilder> update)
            where TItemBuilder : JsonObjectBuilder<TItemBuilder>, new()
        {
            return new Updated(
                array =>
                {
                    JObject item = (JObject)((JArray)array)[index];
                    var builder = new TItemBuilder();
                    builder.Rebase(item);
                    update(builder);
                    return array;
                });
        }

        public static Updated By(Func<JToken, JToken> update)
        {
            return new Updated(update);
        }

        public static Updated By<T>(Func<T, JToken> update)
        {
            return new Updated(v => update(v.Value<T>()));
        }

        public JToken Update(JToken jToken)
        {
            return _function(jToken);
        }
    }
}
