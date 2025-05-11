using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    public interface IInputRebinder
    {
        /// <summary>
        /// 進行重新綁定並回傳結果
        /// </summary>
        /// <param name="bindings"></param>
        /// <param name="uuid"></param>
        /// <param name="rebind"></param>
        /// <returns></returns>
        public InputRebindResult Rebind(IInputBindings bindings, int uuid, EInputCode rebind);
    }
}
