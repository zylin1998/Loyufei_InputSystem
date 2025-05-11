using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEngine.EventSystems.StandaloneInputModule;
using static UnityEngine.UI.InputField;

namespace Loyufei.InputSystem
{
    internal class NoneRebinder : IInputRebinder
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
