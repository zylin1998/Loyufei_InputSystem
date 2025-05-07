using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loyufei.InputSystem.Test
{
    public class RunTimeTest : MonoBehaviour
    {
        public IInput Input { get; private set; }

        private void Start()
        {
            Input = InputManager.FetchInput(1);
        }

        private void Update()
        {
            Debug.Log(Input.GetKey("Event"));
            //Debug.Log(new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")));
            Debug.Log(new Vector2(Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal")));
        }
    }
}
