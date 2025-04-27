using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loyufei.InputSystem
{
    internal class MonoDefaultInput : MonoBehaviour, IInputList
    {
        [SerializeField]
        private List<InputPair> _Pairs = new();

        private void Awake()
        {
            InputManager.DefaultInputs = this;
        }

        public IEnumerable<InputPair> GetPairs()
        {
            return _Pairs;
        }

        public void Init(IInputList inputList)
        {
            _Pairs.AddRange(inputList.GetPairs());
        }
    }
}
