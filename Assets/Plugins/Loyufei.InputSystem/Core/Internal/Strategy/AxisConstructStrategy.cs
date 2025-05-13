using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    internal static class AxisConstructStrategy
    {
        internal static Dictionary<object, IAxisConstructor> Strategy { get; } = new()
        {
            { EInputType.KeyBoard      , new KeyCodeAxisConstructor()        },
            { EInputType.GameController, new GameControllerAxisConstructor() },
        };

        internal static IAxisConstructor GetConstructor(EInputType inputType)
        {
            if (inputType == EInputType.Mobile)
            {
                return default;
            }

            var exist = Strategy.TryGetValue(inputType, out var constructor);

            return exist ? constructor : default;
        }

        internal static bool AddStrategy(object key, IAxisConstructor constructor) 
        {
            return Strategy.TryAdd(key, constructor);
        }

        internal static bool RemoveStrategy(object key) 
        {
            return Strategy.Remove(key);
        }
    }
}
