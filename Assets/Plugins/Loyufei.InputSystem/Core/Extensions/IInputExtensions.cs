using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Loyufei.InputSystem
{
    public static class IInputExtensions
    {
        /// <summary>
        /// 取得輸入軸浮點數數值
        /// </summary>
        /// <param name="self"></param>
        /// <param name="axisName"></param>
        /// <returns></returns>
        public static float GetAxis(this IInput self, string axisName) 
        {
            return self[axisName].Axis;
        }

        /// <summary>
        /// 取得輸入軸浮點數整數數值
        /// </summary>
        /// <param name="self"></param>
        /// <param name="axisName"></param>
        /// <returns></returns>
        public static float GetAxisRaw(this IInput self, string axisName)
        {
            return self[axisName].AxisRaw;
        }

        /// <summary>
        /// 取得輸入軸是否按下
        /// </summary>
        /// <param name="self"></param>
        /// <param name="axisName"></param>
        /// <returns></returns>
        public static bool GetKeyDown(this IInput self, string axisName)
        {
            return self[axisName].KeyDown;
        }

        /// <summary>
        /// 取得輸入軸是否按壓中
        /// </summary>
        /// <param name="self"></param>
        /// <param name="axisName"></param>
        /// <returns></returns>
        public static bool GetKey(this IInput self, string axisName)
        {
            return self[axisName].Key;
        }

        /// <summary>
        /// 取得輸入軸是否放開
        /// </summary>
        /// <param name="self"></param>
        /// <param name="axisName"></param>
        /// <returns></returns>
        public static bool GetKeyUp(this IInput self, string axisName)
        {
            return self[axisName].KeyUp;
        }

        /// <summary>
        /// Unity Input.GetKeyDown
        /// </summary>
        /// <param name="self"></param>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        public static bool GetKeyDown(this IInput self, KeyCode keyCode) 
        {
            return Input.GetKeyDown(keyCode);
        }

        /// <summary>
        /// Unity Input.GetKey
        /// </summary>
        /// <param name="self"></param>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        public static bool GetKey(this IInput self, KeyCode keyCode)
        {
            return Input.GetKey(keyCode);
        }

        /// <summary>
        /// Unity Input.GetKeyUp
        /// </summary>
        /// <param name="self"></param>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        public static bool GetKeyUp(this IInput self, KeyCode keyCode)
        {
            return Input.GetKeyUp(keyCode);
        }

        /// <summary>
        /// 確認是否有按鍵被按下
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool AnyKeyDown(this IInput self)
        {
            return Input.anyKeyDown;
        }

        /// <summary>
        /// 確認是否有按鍵被按壓中
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool AnyKey(this IInput self)
        {
            return Input.anyKey;
        }

        #region InputManager Method Adapt

        /// <summary>
        /// 更改指定輸入
        /// </summary>
        /// <param name="listIndex"></param>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public static async Task<InputRebindResult> Rebind(this IInput self, int uuid)
        {
            return await InputManager.Rebind(self, uuid);
        }

        /// <summary>
        /// 切換輸入頻道的綁定清單
        /// </summary>
        /// <param name="self"></param>
        /// <param name="listIndex"></param>
        /// <returns></returns>
        public static bool SwitchList(this IInput self, int listIndex)
        {
            return InputManager.SwitchList(self, listIndex);
        }

        /// <summary>
        /// 取得該輸入頻道引用的清單
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static IInputList FetchList(this IInput self) 
        {
            if (self is IInputBinder binder) 
            {
                return InputManager.FetchList(binder.Bindings.Index, binder.Bindings.InputType);
            }

            return IInputList.Default;
        }

        /// <summary>
        /// 重製索引及服務平台相對應的輸入清單
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static IEnumerable<InputPair> Reset(this IInput self) 
        {
            if (self is IInputBinder binder)
            {
                return InputManager.ResetList(binder.Bindings.Index, binder.Bindings.InputType);
            }

            return IInputList.Default.GetPairs();
        }

        #endregion
    }
}
