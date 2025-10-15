using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using XInputDotNetPure;

namespace Loyufei.InputSystem
{
    internal abstract class XInputDonetFakeInput : IFakeKey
    {
        private int _HoldFrame    = 0;
        private int _ReleaseFrame = 0;

        public abstract EInputCode InputCode { get; }

        public bool GetKeyDown(int inputIndex)
        {
            if (_HoldFrame == 0) 
            {
                _HoldFrame = GetValue(inputIndex) ? Time.frameCount : 0;
            }

            return _HoldFrame == Time.frameCount;
        }

        public bool GetKey(int inputIndex)
        {
            return GetValue(inputIndex) && _HoldFrame != Time.frameCount;
        }

        public bool GetKeyUp(int inputIndex)
        {
            if (_HoldFrame != 0 && !GetValue(inputIndex)) 
            {
                _HoldFrame    = 0;
                _ReleaseFrame = Time.frameCount;
            }

            return _ReleaseFrame == Time.frameCount;
        }

        protected abstract bool GetValue(int index);

        public static IFakeKey A { get; } = new JoyStickA();
        public static IFakeKey B { get; } = new JoyStickB();
        public static IFakeKey X { get; } = new JoyStickX();
        public static IFakeKey Y { get; } = new JoyStickY();

        public static IFakeKey Start { get; } = new JoyStickStart();
        public static IFakeKey Back  { get; } = new JoyStickBack();

        public static IFakeKey LS_B { get; } = new LeftStick();
        public static IFakeKey RS_B { get; } = new RightStick();

        public static IFakeKey LB { get; } = new LeftShoulder();
        public static IFakeKey RB { get; } = new RightShoulder();

        public static IFakeKey DPADUp    { get; } = new DPADUP();
        public static IFakeKey DPADDown { get; } = new DPADDOWN();
        public static IFakeKey DPADLeft { get; } = new DPADLEFT();
        public static IFakeKey DPADRight { get; } = new DPADRIGHT();

        public static IFakeKey LSUp    { get; } = new ThumbStickLeftUp();
        public static IFakeKey LSDown  { get; } = new ThumbStickLeftDown();
        public static IFakeKey LSLeft  { get; } = new ThumbStickLeftLeft();
        public static IFakeKey LSRight { get; } = new ThumbStickLeftRight();

        public static IFakeKey RSUp    { get; } = new ThumbStickRightUp();
        public static IFakeKey RSDown  { get; } = new ThumbStickRightDown();
        public static IFakeKey RSLeft  { get; } = new ThumbStickRightLeft();
        public static IFakeKey RSRight { get; } = new ThumbStickRightRight();

        public static IFakeKey LT { get; } = new LeftTrigger();
        public static IFakeKey RT { get; } = new RightTrigger();

        public static IFakeKey[] FakeKeys { get; } = new[]
        {
            A, B, X, Y, Start, Back, LS_B, RS_B, LB, RB, DPADUp, DPADDown, DPADLeft, DPADRight, LSUp, LSDown, LSLeft, LSRight, RSUp, RSDown, RSLeft, RSRight, LT, RT,
        };

        public static bool AnyKeyDown(int playerIndex) => FakeKeys.Any(fakeKey => fakeKey.GetKeyDown(playerIndex));
        public static bool AnyKey(int playerIndex) => FakeKeys.Any(fakeKey => fakeKey.GetKey(playerIndex));

        public static IFakeKey GetFakeKey(EInputCode inputCode) 
        {
            return FakeKeys.SingleOrDefault(k => k.InputCode == inputCode) ?? IFakeKey.Default;
        }

        #region Nest Class

        private sealed class JoyStickA : XInputDonetFakeInput 
        {
            public override EInputCode InputCode => EInputCode.JoystickA;

            protected override bool GetValue(int index)
            {
                var playerIndex = (PlayerIndex)index - 1;
                var state       = GamePad.GetState(playerIndex);

                return state.Buttons.A == ButtonState.Pressed;
            }
        }

        private sealed class JoyStickB : XInputDonetFakeInput
        {
            public override EInputCode InputCode => EInputCode.JoystickB;

            protected override bool GetValue(int index)
            {
                var playerIndex = (PlayerIndex)index - 1;
                var state = GamePad.GetState(playerIndex);

                return state.Buttons.B == ButtonState.Pressed;
            }
        }

        private sealed class JoyStickX : XInputDonetFakeInput
        {
            public override EInputCode InputCode => EInputCode.JoystickX;

            protected override bool GetValue(int index)
            {
                var playerIndex = (PlayerIndex)index - 1;
                var state = GamePad.GetState(playerIndex);

                return state.Buttons.X == ButtonState.Pressed;
            }
        }

        private sealed class JoyStickY : XInputDonetFakeInput
        {
            public override EInputCode InputCode => EInputCode.JoystickY;

            protected override bool GetValue(int index)
            {
                var playerIndex = (PlayerIndex)index - 1;
                var state = GamePad.GetState(playerIndex);

                return state.Buttons.Y == ButtonState.Pressed;
            }
        }

        private sealed class JoyStickBack : XInputDonetFakeInput
        {
            public override EInputCode InputCode => EInputCode.Back;

            protected override bool GetValue(int index)
            {
                var playerIndex = (PlayerIndex)index - 1;
                var state = GamePad.GetState(playerIndex);

                return state.Buttons.Back == ButtonState.Pressed;
            }
        }

        private sealed class JoyStickStart : XInputDonetFakeInput
        {
            public override EInputCode InputCode => EInputCode.Start;

            protected override bool GetValue(int index)
            {
                var playerIndex = (PlayerIndex)index - 1;
                var state = GamePad.GetState(playerIndex);

                return state.Buttons.Start == ButtonState.Pressed;
            }
        }

        private sealed class LeftStick : XInputDonetFakeInput
        {
            public override EInputCode InputCode => EInputCode.LS_B;

            protected override bool GetValue(int index)
            {
                var playerIndex = (PlayerIndex)index - 1;
                var state = GamePad.GetState(playerIndex);

                return state.Buttons.LeftStick == ButtonState.Pressed;
            }
        }

        private sealed class RightStick : XInputDonetFakeInput
        {
            public override EInputCode InputCode => EInputCode.RS_B;

            protected override bool GetValue(int index)
            {
                var playerIndex = (PlayerIndex)index - 1;
                var state = GamePad.GetState(playerIndex);

                return state.Buttons.RightStick == ButtonState.Pressed;
            }
        }

        private sealed class LeftShoulder : XInputDonetFakeInput
        {
            public override EInputCode InputCode => EInputCode.LB;

            protected override bool GetValue(int index)
            {
                var playerIndex = (PlayerIndex)index - 1;
                var state = GamePad.GetState(playerIndex);

                return state.Buttons.LeftShoulder == ButtonState.Pressed;
            }
        }

        private sealed class RightShoulder : XInputDonetFakeInput
        {
            public override EInputCode InputCode => EInputCode.RB;

            protected override bool GetValue(int index)
            {
                var playerIndex = (PlayerIndex)index - 1;
                var state = GamePad.GetState(playerIndex);

                return state.Buttons.RightShoulder == ButtonState.Pressed;
            }
        }

        private sealed class DPADUP : XInputDonetFakeInput
        {
            public override EInputCode InputCode => EInputCode.DPADUp;

            protected override bool GetValue(int index)
            {
                var playerIndex = (PlayerIndex)index - 1;
                var state = GamePad.GetState(playerIndex);

                return state.DPad.Up == ButtonState.Pressed;
            }
        }

        private sealed class DPADDOWN : XInputDonetFakeInput
        {
            public override EInputCode InputCode => EInputCode.DPADDown;

            protected override bool GetValue(int index)
            {
                var playerIndex = (PlayerIndex)index - 1;
                var state = GamePad.GetState(playerIndex);

                return state.DPad.Down == ButtonState.Pressed;
            }
        }

        private sealed class DPADLEFT : XInputDonetFakeInput
        {
            public override EInputCode InputCode => EInputCode.DPADLeft;

            protected override bool GetValue(int index)
            {
                var playerIndex = (PlayerIndex)index - 1;
                var state = GamePad.GetState(playerIndex);

                return state.DPad.Left == ButtonState.Pressed;
            }
        }

        private sealed class DPADRIGHT : XInputDonetFakeInput
        {
            public override EInputCode InputCode => EInputCode.DPADRight;

            protected override bool GetValue(int index)
            {
                var playerIndex = (PlayerIndex)index - 1;
                var state = GamePad.GetState(playerIndex);

                return state.DPad.Right == ButtonState.Pressed;
            }
        }

        private sealed class ThumbStickLeftUp : XInputDonetFakeInput
        {
            public override EInputCode InputCode => EInputCode.LSUp;

            protected override bool GetValue(int index)
            {
                var playerIndex = (PlayerIndex)index - 1;
                var state = GamePad.GetState(playerIndex);

                return state.ThumbSticks.Left.Y > 0;
            }
        }

        private sealed class ThumbStickLeftDown : XInputDonetFakeInput
        {
            public override EInputCode InputCode => EInputCode.LSDown;

            protected override bool GetValue(int index)
            {
                var playerIndex = (PlayerIndex)index - 1;
                var state = GamePad.GetState(playerIndex);

                return state.ThumbSticks.Left.Y < 0;
            }
        }

        private sealed class ThumbStickLeftLeft : XInputDonetFakeInput
        {
            public override EInputCode InputCode => EInputCode.LSLeft;

            protected override bool GetValue(int index)
            {
                var playerIndex = (PlayerIndex)index - 1;
                var state = GamePad.GetState(playerIndex);

                return state.ThumbSticks.Left.X < 0;
            }
        }

        private sealed class ThumbStickLeftRight : XInputDonetFakeInput
        {
            public override EInputCode InputCode => EInputCode.LSRight;

            protected override bool GetValue(int index)
            {
                var playerIndex = (PlayerIndex)index - 1;
                var state = GamePad.GetState(playerIndex);

                return state.ThumbSticks.Left.X > 0;
            }
        }

        private sealed class ThumbStickRightUp : XInputDonetFakeInput
        {
            public override EInputCode InputCode => EInputCode.RSUp;

            protected override bool GetValue(int index)
            {
                var playerIndex = (PlayerIndex)index - 1;
                var state = GamePad.GetState(playerIndex);

                return state.ThumbSticks.Right.Y > 0;
            }
        }

        private sealed class ThumbStickRightDown : XInputDonetFakeInput
        {
            public override EInputCode InputCode => EInputCode.RSDown;

            protected override bool GetValue(int index)
            {
                var playerIndex = (PlayerIndex)index - 1;
                var state = GamePad.GetState(playerIndex);

                return state.ThumbSticks.Right.Y < 0;
            }
        }

        private sealed class ThumbStickRightLeft : XInputDonetFakeInput
        {
            public override EInputCode InputCode => EInputCode.RSLeft;

            protected override bool GetValue(int index)
            {
                var playerIndex = (PlayerIndex)index - 1;
                var state = GamePad.GetState(playerIndex);

                return state.ThumbSticks.Right.X < 0;
            }
        }

        private sealed class ThumbStickRightRight : XInputDonetFakeInput
        {
            public override EInputCode InputCode => EInputCode.RSRight;

            protected override bool GetValue(int index)
            {
                var playerIndex = (PlayerIndex)index - 1;
                var state = GamePad.GetState(playerIndex);

                return state.ThumbSticks.Right.X > 0;
            }
        }

        private sealed class LeftTrigger : XInputDonetFakeInput
        {
            public override EInputCode InputCode => EInputCode.LT;

            protected override bool GetValue(int index)
            {
                var playerIndex = (PlayerIndex)index - 1;
                var state = GamePad.GetState(playerIndex);

                return state.Triggers.Left > 0;
            }
        }

        private sealed class RightTrigger : XInputDonetFakeInput
        {
            public override EInputCode InputCode => EInputCode.RT;

            protected override bool GetValue(int index)
            {
                var playerIndex = (PlayerIndex)index - 1;
                var state = GamePad.GetState(playerIndex);

                return state.Triggers.Right > 0;
            }
        }

        #endregion
    }
}
