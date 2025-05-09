using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loyufei.InputSystem
{
    internal class MonoDefaultInput : MonoBehaviour, IInputList
    {
        [SerializeField]
        private List<InputPair> _Pairs = new();
        [SerializeField]
        private EInputType _InputType = EInputType.KeyBoard;

        public EInputType InputType => _InputType;

        private void Awake()
        {
            InputManager.SetDefault(this);
        }

        public void Init(IInputList inputList)
        {
            _Pairs.AddRange(inputList.GetPairs());
        }

        public IEnumerable<InputPair> GetPairs()
        {
            return _Pairs;
        }
    }
}
