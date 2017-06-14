using System;

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
        internal JsonObjectBuilder() { }

        public static implicit operator string(JsonObjectBuilder builder)
        {
            return builder.ToString();
        }
        public override string ToString()
        {
            return "{}";
        }
    }
}
