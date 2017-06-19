using System;
using Newtonsoft.Json.Linq;

namespace csMACnz.FluentJsonBuilder
{
    public class SetTo
    {
        Func<JToken> _function;

        private SetTo(Func<JToken> function)
        {
            _function = function;
        }

        public static SetTo Value(JToken v)
        {
            return new SetTo(() => v);
        }

        public static SetToFunc Null => () => new SetTo(() => null);

        public static SetToFunc RandomGuid => () =>
        {
            return new SetTo(() => Guid.NewGuid().ToString().ToUpper());
        };

        public static SetToFunc True => () =>
        {
            return new SetTo(() => true);
        };

        public static SetToFunc False => () =>
        {
            return new SetTo(() => false);
        };

        public static SetToFunc EmptyArray => () =>
        {
            return new SetTo(() => new JArray());
        };

        public JToken GetValue()
        {
            return _function();
        }
        public delegate SetTo SetToFunc();
        public static implicit operator SetTo(SetToFunc valueFunc)
        {
            return valueFunc();
        }
    }
}
