using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loyufei.InputSystem
{
    public class MonoDefaultInput : MonoBehaviour, IInputList
    {
        [SerializeField]
        private int _Layer;
        [SerializeField]
        private List<InputPair> _Pairs = new();
        [SerializeField]
        private EInputType _InputType;

        public EInputType InputType => _InputType;

        private void Awake()
        {
            InputManager.SetDefault(this, _Layer);
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
