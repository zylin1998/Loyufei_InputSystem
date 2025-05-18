using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if LOYUFEI_INPUTSYSTEM

namespace Loyufei.InputSystem.RuntimeTest
{
    public class UIMainComponent : MonoBehaviour
    {
        [SerializeField]
        private GameObject _Setting;

        public IInput Input { get; private set; }

        void Start()
        {
            Input = InputManager.FetchInput(GlobalParameter.PlayerIndex, GlobalParameter.InputType);
        }

        void Update()
        {
            if (Input.GetKeyDown("Cancel"))
            {
                gameObject.SetActive(false);

                _Setting.SetActive(true);
            }
        }
    }
}

#endif
