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

    public abstract class SetTo
    {
        public static SetTo Value(JToken v)
        {
            return new ValueSetter(v);
        }

        internal abstract JToken GetValue();

        private sealed class ValueSetter : SetTo
        {
            private JToken _value;
            internal ValueSetter(JToken value)
            {
                _value = value;
            }
            internal override JToken GetValue()
            {
                return _value;
            }
        }
    }
}
