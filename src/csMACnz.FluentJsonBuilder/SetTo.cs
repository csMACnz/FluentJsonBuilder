using System;
using Newtonsoft.Json.Linq;

namespace csMACnz.FluentJsonBuilder
{
    public class SetTo
    {
        Func<JToken> _function;

        private SetTo(Func<JToken> function)
        {
            _function = function;
        }

        public static SetTo Value(JToken v)
        {
            return new SetTo(() => v);
        }

        public static SetToFunc Null => () => new SetTo(() => null);

        public static SetToFunc RandomGuid => () =>
        {
            return new SetTo(() => Guid.NewGuid().ToString().ToUpper());
        };

        public static SetToFunc True => () =>
        {
            return new SetTo(() => true);
        };

        public static SetToFunc False => () =>
        {
            return new SetTo(() => false);
        };

        public static SetToFunc AnEmptyArray => () =>
        {
            return new SetTo(() => new JArray());
        };

        public JToken GetValue()
        {
            return _function();
        }
        public delegate SetTo SetToFunc();
        public static implicit operator SetTo(SetToFunc valueFunc)
        {
            return valueFunc();
        }

        public static SetTo AnArrayContaining(params Action<JsonObjectBuilder>[] setValues)
        {
            return AnArrayContaining<JsonObjectBuilder>(setValues);
        }

        public static SetTo AnArrayContaining<TItemBuilder>(params Action<TItemBuilder>[] setValues)
            where TItemBuilder : JsonObjectBuilder<TItemBuilder>, new()
        {
            return new SetTo(() =>
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
                    var builder= new TItemBuilder();
                    builder.Rebase(item);
                    update(builder);
                    return array;
                });
        }

        public JToken Update(JToken jToken)
        {
            return _function(jToken);
        }
    }
}
