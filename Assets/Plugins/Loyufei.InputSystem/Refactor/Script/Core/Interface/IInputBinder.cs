using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    internal interface IInputBinder
    {
        public void Binding(IInputAxis axis, IInputBindings bindings);
    }
}
