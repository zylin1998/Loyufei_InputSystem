using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    public struct AxisValue
    {
        public float Axis    { get; }
        public float AxisRaw { get; }
        public bool  KeyDown { get; }
        public bool  Key     { get; }
        public bool  KeyUp   { get; }

        internal AxisValue(float axis, float axisRaw, bool keyDown, bool key, bool keyUp) 
        {
            Axis    = axis;
            AxisRaw = axisRaw;
            KeyDown = keyDown;
            Key     = key;
            KeyUp   = keyUp;
        }
    }
}
