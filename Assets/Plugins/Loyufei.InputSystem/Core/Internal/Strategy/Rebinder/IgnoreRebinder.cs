﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEngine.UI.InputField;
using UnityEngine.UIElements;

namespace Loyufei.InputSystem
{
    internal struct IgnoreRebinder : IInputRebinder
    {
        public InputRebindResult Rebind(IInputBindings bindings, int uuid, EInputCode rebind)
        {
            var target = bindings[uuid];
            var same   = bindings[rebind];

            target.Reset(rebind);

            return new(true, bindings.Index, bindings.InputType, target, same);
        }
    }
}
