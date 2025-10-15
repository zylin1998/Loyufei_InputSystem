using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    /// <summary>
    /// 確認當前以按壓的輸入
    /// </summary>
    public interface IInputCodeChecker
    {
        public EInputCode Check(int playerIndex);
    }
}
