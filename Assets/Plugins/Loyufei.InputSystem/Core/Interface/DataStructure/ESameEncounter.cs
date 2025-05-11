using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    /// <summary>
    /// 更換輸入重疊時動作種類
    /// </summary>
    public enum ESameEncounter
    {
        None     = 0,
        Ignore   = 1,
        Delete   = 2,
        Exchange = 3,
    }
}
