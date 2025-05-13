using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Loyufei.InputSystem
{
    [CreateAssetMenu(fileName = "Default Input", menuName = "Loyufei InputSystem/Default Input", order = 1)]
    public class ScriptableObjectDefaultInput : ScriptableObject, IInputList
    {
        [SerializeField]
        private List<InputPair> _Pairs = new();
        [SerializeField]
        private EInputType      _InputType;

        public EInputType InputType => _InputType;

        public void SetAsDefault()
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

        private void Reset()
        {
            if (_InputType == EInputType.KeyBoard)
            {
                _Pairs = new()
                {
                    new (1, EInputCode.Return),
                    new (2, EInputCode.Escape),
                };
            }

            if (_InputType == EInputType.GameController)
            {
                _Pairs = new()
                {
                    new (1, EInputCode.Start),
                    new (2, EInputCode.Back),
                };
            }
        }
    }
}
