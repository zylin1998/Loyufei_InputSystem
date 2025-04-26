using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputSystem
{
    public partial class InputManager
    {
        #region Private Static Field

        private static int _Index;

        #endregion

        #region Const Fields

        /// <summary>
        /// 最小輸入存取總數
        /// </summary>
        public const int minIndex = 1;

        /// <summary>
        /// 最大輸入存取總數
        /// </summary>
        public const int maxIndex = 4;

        #endregion

        #region Static Properties

        /// <summary>
        /// 自訂輸入存取數量
        /// </summary>
        public static int IndexCount
        {
            get => _Index;

            set
            {
                _Index = value < minIndex ? minIndex : value;

                _Index = value > maxIndex ? maxIndex : value;
            }
        }

        /// <summary>
        /// 初始輸入欄
        /// </summary>
        public static IInputList DefaultInputs { get; set; } = IInputList.Default;

        /// <summary>
        /// 輸入更換重疊時動作選擇
        /// </summary>
        public static ESameEncounter SameEncounter { get; set; } = ESameEncounter.None;

        #endregion
    }
}
