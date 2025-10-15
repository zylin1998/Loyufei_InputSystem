using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    internal struct NoneRebinder : IInputRebinder
    {
        public InputRebindResult Rebind(IInputBindings bindings, int uuid, EInputCode rebind)
        {
            var target = bindings[uuid];
            
            if (bindings.TryGet(rebind, out var same)) 
            {
                return new(false, bindings.Index, bindings.InputType, target, same);
            }

            target.Reset(rebind);

            return new(true, bindings.Index, bindings.InputType, target, same);
        }
    }
}
