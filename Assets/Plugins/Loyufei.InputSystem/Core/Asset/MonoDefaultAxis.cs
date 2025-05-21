using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loyufei.InputSystem
{
    public class MonoDefaultAxis : MonoBehaviour, IInputAxis
    {
        [SerializeField, Min(-1)]
        private int _Layer;
        [SerializeField]
        private List<AxisPair> _Axis = new();

        public int Layer => _Layer;

        private void Awake() 
        {
            InputManager.SetDefault(this, Layer);
        }

        public IEnumerable<AxisPair> GetPairs()
        {
            return _Axis;
        }
    }
}
