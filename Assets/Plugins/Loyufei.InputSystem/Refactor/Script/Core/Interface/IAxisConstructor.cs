using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loyufei.InputSystem
{
    public interface IAxisConstructor
    {
        public IAxis Construct(AxisPair pair, IInputBindings bindings);
    }
}
