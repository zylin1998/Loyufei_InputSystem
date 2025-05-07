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
            var list = Enum
                .GetValues(typeof(EInputCode))
                .OfType<KeyCode>()
                .ToArray();

            return (EInputCode)list.FirstOrDefault(key => Input.GetKeyDown(key));
        }
    }
}
