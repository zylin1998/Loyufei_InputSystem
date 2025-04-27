using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    internal class InputBase : IInput, IInputBinder
    {
        public AxisValue this[string axisName] 
            => AxisDictionary.TryGetValue(axisName, out var axis) ? axis.GetValue() : new AxisValue();

        internal Dictionary<string, IAxis> AxisDictionary { get; } = new();

        public void Binding(IEnumerable<AxisPair> axis, IInputBindings bindings)
        {
            foreach (var pair in axis) 
            {
                AxisDictionary.Add(pair.AxisName, new Axis(pair, bindings));
            }
        }
    }
}
