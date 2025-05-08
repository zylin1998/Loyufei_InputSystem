using UnityEngine;
using XInputDotNetPure;

namespace Loyufei.InputSystem
{
    internal class GameControllerAxis : IAxis
    {
        private int         _Index;
        private AxisPair    _Pair;
        private BindingPair _Positive;
        private BindingPair _Negative;

        private float _HoldTime = 0f;
        private float _ReleasedTime = 0f;
        private float _DeltaTime = 0f;

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

        public float Axis
        {
            get
            {
                var axisRaw = AxisRaw;

                return Mathf.Clamp01(Hold(!Equals(axisRaw, 0f))) * axisRaw;
            }
        }

        public float AxisRaw
        {
            get
            {
                if (GetValue(Positive.InputCode)) { return 1; }

                if (GetValue(Negative.InputCode)) { return -1; }

                return 0;
            }
        }

        public bool KeyDown
        {
            get
            {
                if (GetValue(Positive.InputCode))
                {
                    (_ReleasedTime, _DeltaTime) = _ReleasedTime == 0 ? (0, _DeltaTime) : (0, 0);

                    _DeltaTime = _DeltaTime == 0 ? Time.deltaTime : _DeltaTime;

                    return Hold(true) <= _DeltaTime;
                }

                return false;
            }
        }

        public bool Key
        {
            get
            {
                if (GetValue(Positive.InputCode))
                {
                    return Hold(true) >= _DeltaTime && !KeyDown;
                }

                return false;
            }
        }

        public bool KeyUp
        {
            get
            {
                if (_HoldTime == 0 && _ReleasedTime == 0) { return false; }

                if (!GetValue(Positive.InputCode))
                {
                    (_HoldTime, _DeltaTime) = _HoldTime == 0 ? (0, _DeltaTime) : (0, 0);

                    _DeltaTime = _DeltaTime == 0 ? Time.deltaTime : _DeltaTime;

                    return Release(true) <= _DeltaTime;
                }

                return false;
            }
        }

        private bool GetValue(EInputCode inputCode)
        {
            if((int)inputCode < 600)
            {
                return false;
            }

            return GetValue(GamePad.GetState((PlayerIndex)_Index), inputCode);
        }

        private bool GetValue(GamePadState state, EInputCode key)
        {
            if (key == EInputCode.JoystickA) { return state.Buttons.A == ButtonState.Pressed; }
            if (key == EInputCode.JoystickB) { return state.Buttons.B == ButtonState.Pressed; }
            if (key == EInputCode.JoystickX) { return state.Buttons.X == ButtonState.Pressed; }
            if (key == EInputCode.JoystickY) { return state.Buttons.Y == ButtonState.Pressed; }

            if (key == EInputCode.Start) { return state.Buttons.Start == ButtonState.Pressed; }
            if (key == EInputCode.Back)  { return state.Buttons.Back  == ButtonState.Pressed; }

            if (key == EInputCode.LS_B) { return state.Buttons.LeftStick  == ButtonState.Pressed; }
            if (key == EInputCode.RS_B) { return state.Buttons.RightStick == ButtonState.Pressed; }

            if (key == EInputCode.LB) { return state.Buttons.LeftShoulder  == ButtonState.Pressed; }
            if (key == EInputCode.RB) { return state.Buttons.RightShoulder == ButtonState.Pressed; }

            if (key == EInputCode.DPADUp)    { return state.DPad.Up    == ButtonState.Pressed; }
            if (key == EInputCode.DPADDown)  { return state.DPad.Down  == ButtonState.Pressed; }
            if (key == EInputCode.DPADLeft)  { return state.DPad.Left  == ButtonState.Pressed; }
            if (key == EInputCode.DPADRight) { return state.DPad.Right == ButtonState.Pressed; }

            if (key == EInputCode.LT) { return state.Triggers.Left  > 0; }
            if (key == EInputCode.RT) { return state.Triggers.Right > 0; }

            if (key == EInputCode.LSUp)    { return state.ThumbSticks.Left.Y > 0; }
            if (key == EInputCode.LSDown)  { return state.ThumbSticks.Left.Y < 0; }
            if (key == EInputCode.LSLeft)  { return state.ThumbSticks.Left.X < 0; }
            if (key == EInputCode.LSRight) { return state.ThumbSticks.Left.X > 0; }

            if (key == EInputCode.RSUp)    { return state.ThumbSticks.Right.Y > 0; }
            if (key == EInputCode.RSDown)  { return state.ThumbSticks.Right.Y < 0; }
            if (key == EInputCode.RSLeft)  { return state.ThumbSticks.Right.X < 0; }
            if (key == EInputCode.RSRight) { return state.ThumbSticks.Right.X > 0; }

            return false;
        }

        private float Hold(bool isHold)
        {
            if (!isHold) { return (_HoldTime = 0f); }

            if (_HoldTime == 0) { _HoldTime = Time.realtimeSinceStartup; }

            return Time.realtimeSinceStartup - _HoldTime;
        }

        private float Release(bool isRelease)
        {
            if (!isRelease) { return (_ReleasedTime = 0f); }

            if (_ReleasedTime == 0) { _ReleasedTime = Time.realtimeSinceStartup; }

            return Time.realtimeSinceStartup - _ReleasedTime;
        }

        public AxisValue GetValue()
        {
            return new(Axis, AxisRaw, KeyDown, Key, KeyUp);
        }
    }
}
