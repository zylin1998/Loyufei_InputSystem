using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
