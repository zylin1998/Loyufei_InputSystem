using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loyufei.InputSystem
{
    internal class MonoDefaultAxis : MonoBehaviour, IInputAxis
    {
        [SerializeField]
        private List<AxisPair> _Axis = new();

        private void Awake() 
        {
            InputManager.SetDefault(this);
        }

        public IEnumerable<AxisPair> GetPairs()
        {
            return _Axis;
        }

        private void Reset()
        {
            _Axis = new()
            {
                new("Submit", 1, 0, false, 1f),
                new("Cancel", 2, 0, false, 1f),
            };
        }
    }
}
