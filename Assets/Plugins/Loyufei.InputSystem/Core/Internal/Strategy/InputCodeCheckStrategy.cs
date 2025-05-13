using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Plastic.Newtonsoft.Json.Linq;

namespace Loyufei.InputSystem
{
    internal static class InputCodeCheckStrategy
    {
        internal static Dictionary<object, IInputCodeChecker> Strategy { get; } = new()
        {
            { EInputType.KeyBoard      , new KeyCodeChecker()             },
            { EInputType.GameController, new GameControllerInputChecker() },
        };

        internal static IInputCodeChecker GetChecker(EInputType inputType)
        {
            if (inputType == EInputType.Mobile)
            {
                return default;
            }

            var exist = Strategy.TryGetValue(inputType, out var checker);

            return exist ? checker : default;
        }

        internal static bool AddStrategy(object key, IInputCodeChecker checker)
        {
            return Strategy.TryAdd(key, checker);
        }

        internal static bool RemoveStrategy(object key)
        {
            return Strategy.Remove(key);
        }
    }
}
