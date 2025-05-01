using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    public interface IInputBinder
    {
        /// <summary>
        /// 將輸入軸及榜定資訊結合綁定
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="bindings"></param>
        public void Binding(IEnumerable<AxisPair> axis, IInputBindings bindings);
    }
}
