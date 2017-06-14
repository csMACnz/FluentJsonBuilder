using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace csMACnz.FluentJsonBuilder
{
    public class JsonBuilder
    {
        public static JsonObjectBuilder CreateObject()
        {
            return new JsonObjectBuilder();
        }
    }

    public class JsonObjectBuilder
    {
        private readonly JObject Data;
        internal JsonObjectBuilder()
        {
            Data = new JObject();
        }

        public JsonObjectBuilder With(string propertyName, Func<SetTo> valueTarget)
        {
            return With(propertyName, valueTarget());
        }

        public JsonObjectBuilder With(string propertyName, SetTo valueTarget)
        {
            Data[propertyName] = valueTarget.GetValue();
            return this;
        }

        public static implicit operator string(JsonObjectBuilder builder)
        {
            return builder.ToString();
        }

        public override string ToString()
        {
            return Data.ToString(Formatting.None);
        }
    }

    public class SetTo
    {
        Func<JToken> _function;

        private SetTo(Func<JToken> function)
        {
            _function = function;
        }

        public static SetTo Null()
        {
            return new SetTo(() => null);
        }

        public static SetTo Value(JToken v)
        {
            return new SetTo(() => v);
        }

        public JToken GetValue()
        {
            return _function();
        }
    }
}
