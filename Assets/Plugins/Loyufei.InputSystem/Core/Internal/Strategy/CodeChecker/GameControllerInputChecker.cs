using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XInputDotNetPure;
using UnityEngine;

namespace Loyufei.InputSystem
{
    public struct GameControllerInputChecker : IInputCodeChecker
    {
        public EInputCode Check(int playerIndex) 
        {
            var index = playerIndex - 1;
            
            var state = GamePad.GetState((PlayerIndex)index);

            if (state.Buttons.A == ButtonState.Pressed) { return EInputCode.JoystickA; }
            if (state.Buttons.B == ButtonState.Pressed) { return EInputCode.JoystickB; }
            if (state.Buttons.X == ButtonState.Pressed) { return EInputCode.JoystickX; }
            if (state.Buttons.Y == ButtonState.Pressed) { return EInputCode.JoystickY; }
            
            if (state.Buttons.Start == ButtonState.Pressed) { return EInputCode.Start; }
            if (state.Buttons.Back  == ButtonState.Pressed) { return EInputCode.Back;  }
            
            if (state.Buttons.LeftStick   == ButtonState.Pressed) { return EInputCode.LS_B; }
            if (state.Buttons.RightStick  == ButtonState.Pressed) { return EInputCode.RS_B; }
            
            if (state.Buttons.LeftShoulder  == ButtonState.Pressed) { return EInputCode.LB; }
            if (state.Buttons.RightShoulder == ButtonState.Pressed) { return EInputCode.RB; }
            
            if (state.DPad.Up    == ButtonState.Pressed) { return EInputCode.DPADUp;    }
            if (state.DPad.Down  == ButtonState.Pressed) { return EInputCode.DPADDown;  }
            if (state.DPad.Left  == ButtonState.Pressed) { return EInputCode.DPADLeft;  }
            if (state.DPad.Right == ButtonState.Pressed) { return EInputCode.DPADRight; }

            if (state.ThumbSticks.Left.Y > 0) { return EInputCode.LSUp;    }
            if (state.ThumbSticks.Left.Y < 0) { return EInputCode.LSDown;  }
            if (state.ThumbSticks.Left.X < 0) { return EInputCode.LSLeft;  }
            if (state.ThumbSticks.Left.X > 0) { return EInputCode.LSRight; }

            if (state.ThumbSticks.Right.Y > 0) { return EInputCode.RSUp;    }
            if (state.ThumbSticks.Right.Y < 0) { return EInputCode.RSDown;  }
            if (state.ThumbSticks.Right.X < 0) { return EInputCode.RSLeft;  }
            if (state.ThumbSticks.Right.X > 0) { return EInputCode.RSRight; }

            return EInputCode.None;
        }
    }
}
