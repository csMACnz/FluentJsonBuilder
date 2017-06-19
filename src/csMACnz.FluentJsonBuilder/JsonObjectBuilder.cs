using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace csMACnz.FluentJsonBuilder
{
    public abstract class JsonObjectBuilder<T>
    where T : JsonObjectBuilder<T>
    {
        private readonly JObject Data;
        protected JsonObjectBuilder()
        {
            Data = new JObject();
        }

        internal JObject GetObject()
        {
            return Data;
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

    public class JsonObjectBuilder : JsonObjectBuilder<JsonObjectBuilder>
    {
    }
}
