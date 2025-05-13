using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    internal struct ExchangeRebinder : IInputRebinder
    {
        public InputRebindResult Rebind(IInputBindings bindings, int uuid, EInputCode rebind)
        {
            var target = bindings[uuid];

            if (bindings.TryGet(rebind, out var same))
            {
                same.Reset(target.InputCode);
            }

            target.Reset(rebind);

            return new(true, bindings.Index, bindings.InputType, target, same);
        }
    }
}
