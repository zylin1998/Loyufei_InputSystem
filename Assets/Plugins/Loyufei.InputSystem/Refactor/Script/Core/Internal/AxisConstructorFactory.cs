using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    internal static class AxisConstructorFactory
    {
        internal static Dictionary<EInputType, IAxisConstructor> Constructors { get; } = new()
        {
            { EInputType.KeyBoard      , new KeyCodeAxisConstructor()        },
            { EInputType.GameController, new GameControllerAxisConstructor() },
        };

        internal static IAxisConstructor Create(EInputType inputType) 
        {
            if (inputType == EInputType.Mobile) 
            {
                return default;
            }

            var exitst = Constructors.TryGetValue(inputType, out var constructor);

            return exitst ? constructor : default;
        }
    }
}
