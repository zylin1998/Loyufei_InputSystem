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
        /// 顯示該清單服務的輸入平台。
        /// </summary>
        public EInputType InputType { get; }

        /// <summary>
        /// 以通用唯一辨識碼尋找綁定資訊
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public BindingPair this[int uuid] { get; }

        /// <summary>
        /// 以輸入尋找綁定資訊
        /// </summary>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        public BindingPair this[EInputCode inputCode] { get; }

        /// <summary>
        /// 嘗試以通用唯一辨識碼尋找綁定資訊
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="binding"></param>
        /// <returns></returns>
        public bool TryGet(int uuid, out BindingPair binding);

        /// <summary>
        /// 嘗試輸入尋找綁定資訊
        /// </summary>
        /// <param name="inputCode"></param>
        /// <param name="binding"></param>
        /// <returns></returns>
        public bool TryGet(EInputCode inputCode, out BindingPair binding);

        /// <summary>
        /// 重新綁定輸入
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="inputCode"></param>
        /// <param name="sameEncounter"></param>
        /// <returns></returns>
        public InputRebindResult Rebinding(int uuid, EInputCode inputCode, ESameEncounter sameEncounter);
    }
}
