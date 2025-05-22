using UnityEngine;
using XInputDotNetPure;
using static UnityEngine.EventSystems.StandaloneInputModule;

namespace Loyufei.InputSystem
{
    internal class GameControllerAxis : IAxis
    {
        private int         _Index;
        private AxisPair    _Pair;
        private BindingPair _Positive;
        private BindingPair _Negative;
        
        public AxisPair    Pair     => _Pair;
        public BindingPair Positive => _Positive;
        public BindingPair Negative => _Negative;

        public GameControllerAxis(AxisPair pair, IInputBindings list)
        {
            _Pair  = pair;
            _Index = list.Index;

            list.TryGet(Pair.PositiveIndex, out _Positive);
            list.TryGet(Pair.NegativeIndex, out _Negative);
        }

        private float _AxisRaw;

        private float _PressTime;

        public float Axis
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
                if ( Positive.FakeKey.GetKey(_Index) && _AxisRaw == 0f) { return (_AxisRaw = 1f); }
                if ( Negative.FakeKey.GetKey(_Index) && _AxisRaw == 0f) { return (_AxisRaw = -1f); }
                if (!Positive.FakeKey.GetKey(_Index) && _AxisRaw  > 0f) { return (_AxisRaw = 0f); }
                if (!Negative.FakeKey.GetKey(_Index) && _AxisRaw  < 0f) { return (_AxisRaw = 0f); }

                return _Pair.Revert ? _AxisRaw * -1 : _AxisRaw;
            }
        }

        public bool KeyDown => Positive.FakeKey.GetKeyDown(_Index);

        public bool Key     => Positive.FakeKey.GetKey(_Index);

        public bool KeyUp   => Positive.FakeKey.GetKeyUp(_Index);

        public AxisValue GetValue()
        {
            return new(Axis, AxisRaw, KeyDown, Key, KeyUp);
        }
    }
}
