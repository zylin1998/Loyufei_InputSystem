using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#if LOYUFEI_INPUTSYSTEM

namespace Loyufei.InputSystem.RuntimeTest
{
    public class InputChanger : MonoBehaviour
    {
        [SerializeField]
        private Text   _Content;
        [SerializeField]
        private Button _Button;
        [SerializeField]
        private int    _UUID;

        public int UUID => _UUID;

        public void AddListener(UnityAction callback) 
        {
            _Button.onClick.AddListener(callback);
        }

        public void SetContext(EInputCode code) 
        {
            _Content.text = code.ToString();
        }

        public void Selected()
        {
            _Button.Select();
        }
    }
}

#endif