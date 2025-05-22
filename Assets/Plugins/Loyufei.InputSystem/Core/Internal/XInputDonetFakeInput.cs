using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using XInputDotNetPure;

namespace Loyufei.InputSystem
{
    internal class XInputDonetFakeInput : IFakeKey
    {
        private int _HoldFrame    = 0;
        private int _ReleaseFrame = 0;

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

        protected virtual bool GetValue(int index) 
        {
            return false;
        }

        private sealed class JoyStickA : XInputDonetFakeInput 
        {
            protected override bool GetValue(int index)
            {
                var playerIndex = (PlayerIndex)index - 1;
                var state       = GamePad.GetState(playerIndex);

                return state.Buttons.A == ButtonState.Pressed;
            }
        }

        private sealed class JoyStickB : XInputDonetFakeInput
        {
            protected override bool GetValue(int index)
            {
                var playerIndex = (PlayerIndex)index - 1;
                var state = GamePad.GetState(playerIndex);

                return state.Buttons.B == ButtonState.Pressed;
            }
        }

        private sealed class JoyStickX : XInputDonetFakeInput
        {
            protected override bool GetValue(int index)
            {
                var playerIndex = (PlayerIndex)index - 1;
                var state = GamePad.GetState(playerIndex);

                return state.Buttons.X == ButtonState.Pressed;
            }
        }

        private sealed class JoyStickY : XInputDonetFakeInput
        {
            protected override bool GetValue(int index)
            {
                var playerIndex = (PlayerIndex)index - 1;
                var state = GamePad.GetState(playerIndex);

                return state.Buttons.Y == ButtonState.Pressed;
            }
        }

        private sealed class JoyStickBack : XInputDonetFakeInput
        {
            protected override bool GetValue(int index)
            {
                var playerIndex = (PlayerIndex)index - 1;
                var state = GamePad.GetState(playerIndex);

                return state.Buttons.Back == ButtonState.Pressed;
            }
        }

        private sealed class JoyStickStart : XInputDonetFakeInput
        {
            protected override bool GetValue(int index)
            {
                var playerIndex = (PlayerIndex)index - 1;
                var state = GamePad.GetState(playerIndex);

                return state.Buttons.Start == ButtonState.Pressed;
            }
        }
    }
}
