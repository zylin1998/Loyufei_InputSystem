using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private void Start()
        {
            var package = InputManager.FetchLists();

            Input = InputManager.FetchInput(1, EInputType.KeyBoard);
        }

        private void Update()
        {
            _Direction = new Vector3(Input.GetAxisRaw("Vertical"), 0f, Input.GetAxisRaw("Horizontal"));

            _Rotation  = Vector3.SignedAngle(_Direction, Vector3.right, Vector3.one);
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
    }
}
