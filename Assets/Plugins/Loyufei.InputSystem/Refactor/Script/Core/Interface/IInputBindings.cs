using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    internal interface IInputBindings
    {
        public BindingPair this[int uuid] { get; }

        public bool TryGet(int uuid, out BindingPair binding);
    }
}
