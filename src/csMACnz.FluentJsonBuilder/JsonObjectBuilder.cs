using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace csMACnz.FluentJsonBuilder
{
    public abstract class JsonObjectBuilder<T>
        where T : JsonObjectBuilder<T>
    {
        private JObject Data;

        protected JsonObjectBuilder()
        {
            Data = new JObject();
        }

        internal JObject GetObject()
        {
            return Data;
        }

        internal void Rebase(JObject item)
        {
            Data = item;
        }

        public T With(string propertyName)
        {
            return With(propertyName, SetTo.Null);
        }

        public T And(string propertyName)
        {
            return With(propertyName);
        }

        public T Without(string propertyName)
        {
            Data.Remove(propertyName);
            return (T) this;
        }

        public T With(string propertyName, Modifier modifier)
        {
            Data[propertyName] = modifier.Modify(Data.TryGetValue(propertyName, out var token) ? token : null);
            return (T) this;
        }

        public T And(string propertyName, Modifier valueTarget)
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