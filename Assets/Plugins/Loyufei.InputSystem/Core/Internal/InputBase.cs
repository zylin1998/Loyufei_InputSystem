using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    internal class InputBase : IInput, IInputBinder
    {
        #region Constructor

        public InputBase() 
        {

        }

        public InputBase(IAxisConstructor constructor, EInputType inputType, int index)
        {
            AxisConstructor = constructor;
            InputType       = inputType;
            Index           = index;
        }

        #endregion

        #region Internal Properties

        internal Dictionary<string, IAxis> AxisDictionary { get; } = new();

        #endregion

        #region Public Properties

        public AxisValue this[string axisName] 
            => AxisDictionary.TryGetValue(axisName, out var axis) ? axis.GetValue() : new AxisValue();

        public int              Index           { get; set; }

        public EInputType       InputType       { get; set; }

        public IAxisConstructor AxisConstructor { get; set; }

        public IInputBindings   Bindings        { get; private set; }

        #endregion

        public void Binding(IEnumerable<AxisPair> axis, IInputBindings bindings)
        {
            Bindings = bindings;

            foreach (var pair in axis) 
            {
                AxisDictionary.Add(pair.AxisName, AxisConstructor.Construct(pair, Bindings));
            }
        }
    }
}
