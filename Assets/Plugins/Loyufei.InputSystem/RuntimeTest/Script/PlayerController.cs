using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

#if LOYUFEI_INPUTSYSTEM

namespace Loyufei.InputSystem.RuntimeTest
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private CharacterController _Controller;
        [SerializeField]
        private Transform           _Model;
        [SerializeField]
        private float               _Speed = 5f;

        public IInput Input { get; private set; }

        private float _RotateSmooth;

        private Vector3 _Direction;
        private float   _Rotation;

        private int _EventHashCode;

        private void Start()
        {
            var package = InputManager.FetchLists();

            Input = InputManager.FetchInput(GlobalParameter.PlayerIndex, GlobalParameter.InputType);

            _EventHashCode = Input.GetHashCode("Event");
        }

        private void Update()
        {
            _Direction = new Vector3(Input.GetAxisRaw("Vertical"), 0f, Input.GetAxisRaw("Horizontal"));

            _Rotation  = Vector3.SignedAngle(_Direction, Vector3.right, Vector3.one);

            Debug.Log(Input.GetKey(_EventHashCode));

            //GameControllerInput();
        }

        private void FixedUpdate()
        {
            if (_Direction == Vector3.zero) { return; }

            ChangeAngleY();

            Move();
        }

        private Vector3 Move() 
        {
            var result = new Vector3(_Direction.x, 0f, -_Direction.z) * Time.fixedDeltaTime * _Speed;

            _Controller.Move(result);

            return result;
        }

        private float ChangeAngleY() 
        {
            var current = _Model.eulerAngles.y;
            var result  = Mathf.SmoothDampAngle(current, _Rotation, ref _RotateSmooth, 0.1f);
            
            _Model.rotation = Quaternion.Euler(0f, result, 0f);

            return result;
        }

        private void GameControllerInput() 
        {
            Debug.Log("GameController Input");

            var state = GamePad.GetState((PlayerIndex)Input.Index - 1);
            
            if (state.Buttons.A == ButtonState.Pressed) { Debug.Log(EInputCode.JoystickA); }
            if (state.Buttons.B == ButtonState.Pressed) { Debug.Log(EInputCode.JoystickB); }
            if (state.Buttons.X == ButtonState.Pressed) { Debug.Log(EInputCode.JoystickX); }
            if (state.Buttons.Y == ButtonState.Pressed) { Debug.Log(EInputCode.JoystickY); }
            
            if (state.Buttons.Start == ButtonState.Pressed) { Debug.Log(EInputCode.Start); }
            if (state.Buttons.Back  == ButtonState.Pressed) { Debug.Log(EInputCode.Back); }

            if (state.Buttons.LeftStick  == ButtonState.Pressed) { Debug.Log(EInputCode.LS_B); }
            if (state.Buttons.RightStick == ButtonState.Pressed) { Debug.Log(EInputCode.RS_B); }

            if (state.Buttons.LeftShoulder  == ButtonState.Pressed) { Debug.Log(EInputCode.LB); }
            if (state.Buttons.RightShoulder == ButtonState.Pressed) { Debug.Log(EInputCode.RB); }

            if (state.DPad.Up    == ButtonState.Pressed) { Debug.Log(EInputCode.DPADUp); }
            if (state.DPad.Down  == ButtonState.Pressed) { Debug.Log(EInputCode.DPADDown); }
            if (state.DPad.Right == ButtonState.Pressed) { Debug.Log(EInputCode.DPADRight); }
            if (state.DPad.Left  == ButtonState.Pressed) { Debug.Log(EInputCode.DPADLeft); }

            if (state.Triggers.Left  > 0) { Debug.Log(EInputCode.LT); }
            if (state.Triggers.Right > 0) { Debug.Log(EInputCode.RT); }

            if (state.ThumbSticks.Left.Y > 0) { Debug.Log(EInputCode.LSUp); }
            if (state.ThumbSticks.Left.Y < 0) { Debug.Log(EInputCode.LSDown); }
            if (state.ThumbSticks.Left.X > 0) { Debug.Log(EInputCode.LSRight); }
            if (state.ThumbSticks.Left.X < 0) { Debug.Log(EInputCode.LSLeft); }

            if (state.ThumbSticks.Right.Y > 0) { Debug.Log(EInputCode.RSUp); }
            if (state.ThumbSticks.Right.Y < 0) { Debug.Log(EInputCode.RSDown); }
            if (state.ThumbSticks.Right.X > 0) { Debug.Log(EInputCode.RSRight); }
            if (state.ThumbSticks.Right.X < 0) { Debug.Log(EInputCode.RSLeft); }
        }
    }
}

#endif