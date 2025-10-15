using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    /// <summary>
    /// 輸入軸，可以取得各輸入軸得值
    /// </summary>
    public interface IAxis
    {
        /// <summary>
        /// 取得輸入軸的值
        /// </summary>
        /// <returns></returns>
        public AxisValue GetValue();
    }
}
