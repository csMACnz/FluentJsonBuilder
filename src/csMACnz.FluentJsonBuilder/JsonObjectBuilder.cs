using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace csMACnz.FluentJsonBuilder
{
    public abstract class JsonObjectBuilder<T>
    where T : JsonObjectBuilder<T>
    {
        private readonly JObject Data;
        protected JsonObjectBuilder()
        : this(new JObject())
        {
        }

        protected JsonObjectBuilder(JObject jObject)
        {
            Data = jObject;
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

        public T With(string propertyName, Updated updateTarget)
        {
            Data[propertyName] = updateTarget.Update(Data[propertyName]);
            return (T)this;
        }

        public T And(string propertyName, Updated updateTarget)
        {
            return With(propertyName, updateTarget);
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
        public JsonObjectBuilder()
        {
        }

        public JsonObjectBuilder(JObject j)
        : base(j)
        {
        }
    }
}
