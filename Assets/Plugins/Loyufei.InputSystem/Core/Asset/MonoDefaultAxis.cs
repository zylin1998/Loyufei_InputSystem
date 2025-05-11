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
    }
}
