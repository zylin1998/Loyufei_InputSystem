using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    public static partial class InputManager
    {
        public static class Monitor 
        {
            public static int ActiveInputs()
            {
                return Instance.Inputs.Count();
            }

            public static int ActiveInputs(EInputType inputType) 
            {
                return Instance.Inputs.Values.Count(input => input.InputType == inputType);
            }

            public static int[] ActiveInputIndex(EInputType inputType) 
            {
                return Instance.Inputs.Values
                    .Where(input => input.InputType == inputType)
                    .Select(input => input.Index)
                    .ToArray();
            }
        }
    }
}
