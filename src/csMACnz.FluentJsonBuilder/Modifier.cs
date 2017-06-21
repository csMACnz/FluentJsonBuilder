using System;
using Newtonsoft.Json.Linq;

namespace csMACnz.FluentJsonBuilder
{
    public sealed class Modifier
    {
        public delegate Modifier ModifierFunc();

        private readonly Func<JToken, JToken> _function;

        internal Modifier(Func<JToken, JToken> function)
        {
            _function = function;
        }

        public static implicit operator Modifier(ModifierFunc valueFunc)
        {
            return valueFunc();
        }


        public JToken Modify(JToken jToken)
        {
            return _function(jToken);
        }
    }
}