using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Loyufei.InputSystem
{
    internal class Axis : IAxis
    {
        private AxisPair    _Pair;
        private BindingPair _Positive;
        private BindingPair _Negative;

        public AxisPair    Pair     => _Pair;
        public BindingPair Positive => _Positive;
        public BindingPair Negative => _Negative;

        public Axis(AxisPair pair, IInputBindings list)
        {
            _Pair = pair;

            list.TryGet(Pair.PositiveIndex, out _Positive);
            list.TryGet(Pair.NegativeIndex, out _Negative);
        }

        private float _AxisRaw;

        private float _PressTime;

        public float AxisFloat
        {
            get 
            {
                if (AxisRaw != 0) 
                {
                    _PressTime = _PressTime == 0 ? Time.realtimeSinceStartup : _PressTime;

                    var passTime = Mathf.Clamp01(Time.realtimeSinceStartup - _PressTime);
                    var result   = Mathf.Clamp01(passTime * (1 / Pair.Sensitive));

                    return AxisRaw * result;
                }

                return 0f;
            }
        }

        public float AxisRaw 
        {
            get 
            {
                if (Input.GetKeyDown(Positive.KeyCode) && _AxisRaw ==  0f) { return (_AxisRaw =  1f); }
                if (Input.GetKeyDown(Negative.KeyCode) && _AxisRaw ==  0f) { return (_AxisRaw = -1f); }
                if (Input.GetKeyUp  (Positive.KeyCode) && _AxisRaw >   0f) { return (_AxisRaw =  0f); }
                if (Input.GetKeyUp  (Negative.KeyCode) && _AxisRaw <   0f) { return (_AxisRaw =  0f); }

                return _AxisRaw;
            }
        }

        public bool KeyDown => Input.GetKeyDown(Positive.KeyCode);
        public bool Key     => Input.GetKey    (Positive.KeyCode);
        public bool KeyUp   => Input.GetKeyUp  (Positive.KeyCode);

        public AxisValue GetValue() => new(AxisFloat, AxisRaw, KeyDown, Key, KeyUp); 
    }
}
