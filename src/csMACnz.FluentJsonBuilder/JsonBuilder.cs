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

        public JsonObjectBuilder With(string propertyName)
        {
            return With(propertyName, SetTo.Null);
        }

        public JsonObjectBuilder And(string propertyName)
        {
            return With(propertyName);
        }

        public JsonObjectBuilder With(string propertyName, SetTo valueTarget)
        {
            Data[propertyName] = valueTarget.GetValue();
            return this;
        }

        public JsonObjectBuilder And(string propertyName, SetTo valueTarget)
        {
            return With(propertyName, valueTarget);
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
}
