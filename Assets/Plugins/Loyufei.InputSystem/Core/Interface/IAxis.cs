using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    public interface IAxis
    {
        /// <summary>
        /// 取得輸入軸的值
        /// </summary>
        /// <returns></returns>
        public AxisValue GetValue();
    }
}
