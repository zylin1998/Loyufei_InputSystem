using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Loyufei.InputSystem
{
    [CreateAssetMenu(fileName = "Default Axis", menuName = "Loyufei InputSystem/Default Axis", order = 1)]
    public class ScriptableObjectDefaultAxis : ScriptableObject, IInputAxis
    {
        [SerializeField]
        private List<AxisPair> _Axis = new();

        public void SetAsDefault()
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
