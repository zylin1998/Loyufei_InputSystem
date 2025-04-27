using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    public partial class InputManager
    {
        #region Public Static Mothods

        public static bool ValidIndex(int index)
        {
            if (index < minIndex) { return false; }
            if (index > maxIndex) { return false; }

            return true;
        }

        public static IInput Fetch(int index) 
        {
            return Instance.GetInput(index);
        }

        #endregion
    }
}
