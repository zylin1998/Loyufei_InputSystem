using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    public interface IInputBindings
    {
        /// <summary>
        /// 輸入清單索引
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// 以通用唯一辨識碼尋找綁定資訊
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public BindingPair this[int uuid] { get; }

        /// <summary>
        /// 嘗試以通用唯一辨識碼尋找綁定資訊
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="binding"></param>
        /// <returns></returns>
        public bool TryGet(int uuid, out BindingPair binding);
    }
}
