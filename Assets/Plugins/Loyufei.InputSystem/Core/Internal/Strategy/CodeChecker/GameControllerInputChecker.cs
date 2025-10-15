using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    public struct GameControllerInputChecker : IInputCodeChecker
    {
        public EInputCode Check(int playerIndex) 
        {
            foreach (var fakeKey in XInputDonetFakeInput.FakeKeys) 
            {
                if (fakeKey.GetKeyDown(playerIndex)) { return fakeKey.InputCode; }
            }
            
            return EInputCode.None;
        }
    }
}
