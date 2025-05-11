using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    internal static class InputCodeCheckStrategy
    {
        internal static Dictionary<EInputType, IInputCodeChecker> Checkers { get; } = new()
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

            var exist = Checkers.TryGetValue(inputType, out var checker);

            return exist ? checker : default;
        }
    }
}
