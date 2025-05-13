using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    internal struct CallbackChecker : IInputCodeChecker
    {
        public CallbackChecker(Func<int, EInputCode> callback) 
        {
            _Callback = callback;
        }

        private Func<int, EInputCode> _Callback;

        public EInputCode Check(int playerIndex)
        {
            return _Callback?.Invoke(playerIndex) ?? EInputCode.None;
        }
    }
}
