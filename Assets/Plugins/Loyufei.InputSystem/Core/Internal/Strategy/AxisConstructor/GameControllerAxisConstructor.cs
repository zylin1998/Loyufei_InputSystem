using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    public struct GameControllerAxisConstructor : IAxisConstructor
    {
        public IAxis Construct(AxisPair pair, IInputBindings bindings)
        {
            return new GameControllerAxis(pair, bindings);
        }
    }
}
