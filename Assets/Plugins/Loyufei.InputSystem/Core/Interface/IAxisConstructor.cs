using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    /// <summary>
    /// 輸入軸建構，根據實做提供不同種類的輸入軸
    /// </summary>
    public interface IAxisConstructor
    {
        public IAxis Construct(AxisPair pair, IInputBindings bindings);
    }
}
