using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    public static class InputRebindStrategy
    {
        internal static Dictionary<object, IInputRebinder> Strategy { get; } = new()
        {
            { ESameEncount.None     , new NoneRebinder()},
            { ESameEncount.Ignore   , new IgnoreRebinder()},
            { ESameEncount.Delete   , new DeleteRebinder()},
            { ESameEncount.Exchange , new ExchangeRebinder()},
        };

        internal static IInputRebinder GetRebinder(ESameEncount encounter) 
        {
            return Strategy[encounter];
        }

        internal static bool AddStrategy(object key, IInputRebinder rebinder)
        {
            return Strategy.TryAdd(key, rebinder);
        }

        internal static bool RemoveStrategy(object key)
        {
            return Strategy.Remove(key);
        }
    }
}
