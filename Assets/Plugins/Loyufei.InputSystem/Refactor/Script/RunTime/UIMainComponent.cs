using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loyufei.InputSystem
{
    public class UIMainComponent : MonoBehaviour
    {
        [SerializeField]
        private GameObject _Setting;

        public IInput Input { get; private set; }
        
        void Start()
        {
            Input = InputManager.FetchInput(1, EInputType.KeyBoard);        
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
