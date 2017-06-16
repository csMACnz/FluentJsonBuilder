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

    public class JsonObjectBuilder : JsonObjectBuilder<JsonObjectBuilder>
    {
        internal JsonObjectBuilder()
        {
        }
    }

    public abstract class JsonObjectBuilder<T>
    where T : JsonObjectBuilder<T>
    {
        private readonly JObject Data;
        protected JsonObjectBuilder()
        {
            Data = new JObject();
        }

        public T With(string propertyName)
        {
            return With(propertyName, SetTo.Null);
        }

        public T And(string propertyName)
        {
            return With(propertyName);
        }

        public T With(string propertyName, SetTo valueTarget)
        {
            Data[propertyName] = valueTarget.GetValue();
            return (T)this;
        }

        public T And(string propertyName, SetTo valueTarget)
        {
            return With(propertyName, valueTarget);
        }

        public static implicit operator string(JsonObjectBuilder<T> builder)
        {
            return builder.ToString();
        }

        public override string ToString()
        {
            return Data.ToString(Formatting.None);
        }
    }
}
