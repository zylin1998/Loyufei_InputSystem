using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    internal interface IFakeKey
    {
        public bool GetKeyDown(int inputIndex);
        public bool GetKey(int inputIndex);
        public bool GetKeyUp(int inputIndex);
    }
}
