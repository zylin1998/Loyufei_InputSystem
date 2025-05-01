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
            { EInputType.KeyBoard, new KeyCodeAxisConstructor() },
        };

        internal static IAxisConstructor Create(EInputType inputType) 
        {
            var exitst = Constructors.TryGetValue(inputType, out var constructor);

            return exitst ? constructor : default;
        }
    }
}
