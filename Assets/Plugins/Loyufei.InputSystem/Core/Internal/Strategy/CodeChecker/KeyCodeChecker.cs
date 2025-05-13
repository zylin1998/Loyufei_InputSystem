using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Loyufei.InputSystem
{
    internal struct KeyCodeChecker : IInputCodeChecker
    {
        public EInputCode Check(int playerIndex) 
        {
            var array = InputManager.AvailableKeyCode;

            return (EInputCode)array.FirstOrDefault(key => Input.GetKeyDown(key));
        }
    }
}
