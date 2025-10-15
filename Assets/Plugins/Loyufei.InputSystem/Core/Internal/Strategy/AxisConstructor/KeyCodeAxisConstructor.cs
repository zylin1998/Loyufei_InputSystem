using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
