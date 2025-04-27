using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loyufei.InputSystem
{
    public static class IInputExtensions
    {
        public static float GetAxis(this IInput self, string axisName) 
        {
            return self[axisName].Axis;
        }

        public static float GetAxisRaw(this IInput self, string axisName)
        {
            return self[axisName].AxisRaw;
        }

        public static bool GetKeyDown(this IInput self, string axisName)
        {
            return self[axisName].KeyDown;
        }

        public static bool GetKey(this IInput self, string axisName)
        {
            return self[axisName].Key;
        }

        public static bool GetKeyUp(this IInput self, string axisName)
        {
            return self[axisName].KeyUp;
        }
    }
}
