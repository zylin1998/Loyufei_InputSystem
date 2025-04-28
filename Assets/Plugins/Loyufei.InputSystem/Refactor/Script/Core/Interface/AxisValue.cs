using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    public struct AxisValue
    {
        /// <summary>
        /// 浮點數輸入值，會根據 deltaTime 及 Sensitive 變化
        /// </summary>
        public float Axis    { get; }
        /// <summary>
        /// 浮點數輸入值，根據輸入鍵回傳 1、0、-1
        /// </summary>
        public float AxisRaw { get; }
        /// <summary>
        /// 關輸入軸之正向鍵是否按下
        /// </summary>
        public bool  KeyDown { get; }
        /// <summary>
        /// 關輸入軸之正向鍵是否按壓
        /// </summary>
        public bool  Key     { get; }
        /// <summary>
        /// 關輸入軸之正向鍵是否放開
        /// </summary>
        public bool  KeyUp   { get; }

        internal AxisValue(float axis, float axisRaw, bool keyDown, bool key, bool keyUp) 
        {
            Axis    = axis;
            AxisRaw = axisRaw;
            KeyDown = keyDown;
            Key     = key;
            KeyUp   = keyUp;
        }
    }
}
