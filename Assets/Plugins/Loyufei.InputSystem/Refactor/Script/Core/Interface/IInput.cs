using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    public interface IInput
    {
        /// <summary>
        /// 以字串尋找對應輸入軸並取得該輸入軸的值
        /// </summary>
        /// <param name="axisName"></param>
        /// <returns></returns>
        public AxisValue this[string axisName] { get; }

        public static IInput Default { get; } = new DefaultInput();

        private class DefaultInput : IInput
        {
            public AxisValue this[string axisName] => new();
        }
    }
}
