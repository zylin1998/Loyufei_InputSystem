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

        internal Dictionary<string, IAxis> AxisNameDictionary { get; } = new();
        internal Dictionary<int   , IAxis> HashCodeDictionary { get; } = new();

        #endregion

        #region Public Properties

        public AxisValue this[string axisName] 
            => AxisNameDictionary.TryGetValue(axisName, out var axis) ? axis.GetValue() : new AxisValue();
        public AxisValue this[int hashCode]
            => HashCodeDictionary.TryGetValue(hashCode, out var axis) ? axis.GetValue() : new AxisValue();

        public int              Index           { get; set; }
        public EInputType       InputType       { get; set; }
        public IAxisConstructor AxisConstructor { get; set; }
        public IInputBindings   Bindings        { get; private set; }

        #endregion

        public int GetHashCode(string axisName) 
        {
            return AxisNameDictionary.TryGetValue(axisName, out var axis) ? axis.GetHashCode() : 0;
        }

        public void Binding(IEnumerable<AxisPair> axisPairs, IInputBindings bindings)
        {
            Bindings = bindings;

            foreach (var pair in axisPairs) 
            {
                var axis = AxisConstructor.Construct(pair, Bindings);

                AxisNameDictionary.Add(pair.AxisName     , axis);

                HashCodeDictionary.Add(axis.GetHashCode(), axis);
            }
        }
    }
}
