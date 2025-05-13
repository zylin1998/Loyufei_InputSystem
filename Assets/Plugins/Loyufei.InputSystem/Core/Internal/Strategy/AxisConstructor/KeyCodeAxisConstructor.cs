using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loyufei.InputSystem
{
    public struct KeyCodeAxisConstructor : IAxisConstructor
    {
        public IAxis Construct(AxisPair pair, IInputBindings bindings)
        {
            return new KeyCodeAxis(pair, bindings);
        }
    }
}
