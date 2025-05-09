using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loyufei.InputSystem.Test
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

        private void Start()
        {
            var package = InputManager.FetchLists();

            Input = InputManager.FetchInput(1);
        }

        private void Update()
        {
            var direct = new Vector3(Input.GetAxisRaw("Vertical"), 0f, Input.GetAxisRaw("Horizontal"));

            var rotation = Vector3.SignedAngle(direct, Vector3.right, Vector3.one);

            _Controller.Move(new Vector3(direct.x, 0f, -direct.z) * Time.deltaTime * _Speed);

            _Model.rotation = Quaternion.Euler(0f, rotation, 0f);
        }
    }
}
