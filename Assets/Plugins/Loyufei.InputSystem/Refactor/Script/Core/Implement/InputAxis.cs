using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    public class InputAxis : IInputAxis
    {
        [NonSerialized]
        private List<AxisPair> _Pairs = new();

        public IEnumerable<AxisPair> GetPairs() => _Pairs;
    }
}
