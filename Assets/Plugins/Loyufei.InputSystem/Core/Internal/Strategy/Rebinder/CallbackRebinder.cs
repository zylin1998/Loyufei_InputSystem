using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    internal struct CallbackRebinder : IInputRebinder
    {
        public CallbackRebinder(Func<IInputBindings, int, EInputCode, InputRebindResult> callback) 
        {
            _Callback = callback;
        }

        private Func<IInputBindings, int, EInputCode, InputRebindResult> _Callback;

        public InputRebindResult Rebind(IInputBindings bindings, int uuid, EInputCode rebind)
        {
            return _Callback?.Invoke(bindings, uuid, rebind) ?? new(false, bindings.Index, bindings.InputType, BindingPair.Default, BindingPair.Default);
        }
    }
}
