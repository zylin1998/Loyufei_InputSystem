using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    /// <summary>
    /// 輸入綁定器，用於將輸入鍵位綁訂至輸入軸
    /// </summary>
    public interface IInputBinder
    {
        public IInputBindings Bindings { get; }

        /// <summary>
        /// 將輸入軸及榜定資訊結合綁定
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="bindings"></param>
        public void Binding(IEnumerable<AxisPair> axis, IInputBindings bindings);
    }
}
