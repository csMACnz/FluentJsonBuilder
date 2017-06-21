using System;
using Newtonsoft.Json.Linq;

namespace csMACnz.FluentJsonBuilder
{
    public sealed class Modifier
    {
        Func<JToken, JToken> _function;

        internal Modifier(Func<JToken, JToken> function)
        {
            _function = function;
        }

        public delegate Modifier ModifierFunc();
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