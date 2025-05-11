using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Loyufei.InputSystem
{
    internal class KeyCodeChecker : IInputCodeChecker
    {
        public EInputCode Check(int playerIndex) 
        {
            var array = Enum
                .GetValues(typeof(EInputCode))
                .Cast<KeyCode>()
                .ToArray();
            
            return (EInputCode)array.FirstOrDefault(key => Input.GetKeyDown(key));
        }
    }
}
