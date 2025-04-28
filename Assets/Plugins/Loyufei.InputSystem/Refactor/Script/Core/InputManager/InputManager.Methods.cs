using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Windows;

namespace Loyufei.InputSystem
{
    public partial class InputManager
    {
        #region Public Static Mothods

        /// <summary>
        /// 設定預設輸入軸資訊
        /// </summary>
        /// <param name="inputAxis"></param>
        public static void SetDefaultAxis(IInputAxis inputAxis) 
        {
            Instance.SetAxis(inputAxis);
        }

        /// <summary>
        /// 判斷輸入索引是否有效
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool IsIndexValid(int index)
        {
            if (index < minIndex) { return false; }
            if (index > maxIndex) { return false; }

            return true;
        }

        /// <summary>
        /// 以索引取得輸入頻道
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static IInput Fetch(int index) 
        {
            if (!IsIndexValid(index)) 
            {
                return IInput.Default;
            }

            var input = Instance.GetInput(index);
            
            if (Instance._UIControl.CurrentIndex == 0) 
            {
                Instance.SetUIControl(index);  
            }

            return input;
        }

        /// <summary>
        /// 嘗試以索引取得輸入頻道
        /// </summary>
        /// <param name="index"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool TryFetch(int index, out IInput input) 
        {
            if (!IsIndexValid(index))
            {
                input = IInput.Default;

                return false;
            }

            input = Instance.GetInput(index);

            if (Instance._UIControl.CurrentIndex == 0)
            {
                Instance.SetUIControl(index);
            }

            return true;
        }

        /// <summary>
        /// 切換UI控制輸入頻道
        /// </summary>
        /// <param name="index"></param>
        public static void SwitchUIControlInput(int index) 
        {
            if (!IsIndexValid(index)) { return; }

            Instance.SetUIControl(index);
        }

        #endregion
    }
}
