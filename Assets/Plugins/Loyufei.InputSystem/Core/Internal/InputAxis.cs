using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Loyufei.InputSystem
{
    public class InputAxis : IInputAxis
    {
        [SerializeField]
        private List<AxisPair> _Pairs = new();

        public IEnumerable<AxisPair> GetPairs() => _Pairs;
    }
}
