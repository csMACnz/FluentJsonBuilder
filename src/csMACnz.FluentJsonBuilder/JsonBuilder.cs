namespace csMACnz.FluentJsonBuilder
{
    public class JsonBuilder
    {
        public static JsonObjectBuilder CreateObject()
        {
            return new JsonObjectBuilder();
        }
    }
}