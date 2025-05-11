using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    public static class InputRebindStrategy
    {
        internal static Dictionary<ESameEncounter, IInputRebinder> Strategy { get; } = new()
        {
            { ESameEncounter.None     , new NoneRebinder()},
            { ESameEncounter.Ignore   , new IgnoreRebinder()},
            { ESameEncounter.Delete   , new DeleteRebinder()},
            { ESameEncounter.Exchange , new ExchangeRebinder()},
        };

        internal static IInputRebinder GetRebinder(ESameEncounter encounter) 
        {
            return Strategy[encounter];
        }
    }
}
