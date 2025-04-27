using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    public interface IInputAxis
    {
        public IEnumerable<AxisPair> GetPairs();

        public static IInputAxis Default { get; } = new DefaultPairs();

        private class DefaultPairs : IInputAxis 
        {
            private List<AxisPair> _Pairs = new();

            public IEnumerable<AxisPair> GetPairs() => _Pairs;
        }
    }
}
