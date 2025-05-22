using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    internal interface IFakeKey
    {
        public EInputCode InputCode { get; }

        public bool GetKeyDown(int inputIndex);
        public bool GetKey(int inputIndex);
        public bool GetKeyUp(int inputIndex);

        public static IFakeKey Default { get; } = new DefaultKey();

        private struct DefaultKey : IFakeKey
        {
            public EInputCode InputCode => EInputCode.None;

            public bool GetKey(int inputIndex)
            {
                return false;
            }

            public bool GetKeyDown(int inputIndex)
            {
                return false;
            }

            public bool GetKeyUp(int inputIndex)
            {
                return false;
            }
        }
    }
}
