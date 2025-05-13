using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    internal struct CallbackConstructor : IAxisConstructor
    {
        public CallbackConstructor(Func<AxisPair, IInputBindings, IAxis> callback) 
        {
            _Callback = callback;
        }

        private Func<AxisPair, IInputBindings, IAxis> _Callback;

        public IAxis Construct(AxisPair pair, IInputBindings bindings)
        {
            return _Callback?.Invoke(pair, bindings);
        }
    }
}
