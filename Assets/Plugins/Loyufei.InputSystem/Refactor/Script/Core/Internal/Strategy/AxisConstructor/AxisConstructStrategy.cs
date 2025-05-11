using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    internal static class AxisConstructStrategy
    {
        internal static Dictionary<EInputType, IAxisConstructor> Constructors { get; } = new()
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

            var exist = Constructors.TryGetValue(inputType, out var constructor);

            return exist ? constructor : default;
        }
    }
}
