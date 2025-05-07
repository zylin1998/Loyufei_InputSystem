using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    public static partial class InputManager
    {
        #region Public Static Mothods

        /// <summary>
        /// 切換UI控制輸入頻道
        /// </summary>
        /// <param name="index"></param>
        public static void SwitchUIControlInput(int index)
        {
            if (!Instance.IsIndexValid(index)) { return; }

            Instance.SetUIControl(index);
        }

        /// <summary>
        /// 設定預設輸入軸資訊
        /// </summary>
        /// <param name="inputAxis"></param>
        public static void SetDefaultAxis(IInputAxis inputAxis) 
        {
            Instance.SetAxis(inputAxis);
        }

        /// <summary>
        /// 以索引取得輸入頻道
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static IInput FetchInput(int index, EInputType inputType = EInputType.KeyBoard) 
        {
            return FetchInput(index, index, inputType);
        }

        /// <summary>
        /// 以索引取得輸入頻道
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static IInput FetchInput(int inputIndex, int listIndex, EInputType inputType = EInputType.KeyBoard)
        {
            if (!Instance.IsIndexValid(inputIndex))
            {
                return IInput.Default;
            }

            var list  = Instance.FetchList(listIndex, inputType);
            var input = Instance.FetchInput(inputIndex, list, inputType);

            if (Instance.UIControl.CurrentIndex == 0)
            {
                SwitchUIControlInput(listIndex);
            }

            return input;
        }

        /// <summary>
        /// 嘗試以索引取得輸入頻道
        /// </summary>
        /// <param name="index"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool TryFetchInput(int index, EInputType inputType, out IInput input) 
        {
            return TryFetchInput(index, index, inputType, out input);
        }

        /// <summary>
        /// 嘗試以索引取得輸入頻道
        /// </summary>
        /// <param name="index"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool TryFetchInput(int inputIndex, int listIndex, EInputType inputType, out IInput input)
        {
            if (!Instance.IsIndexValid(inputIndex))
            {
                input = IInput.Default;

                return false;
            }

            var list = Instance.FetchList(listIndex, inputType);

            input = Instance.FetchInput(inputIndex, list, inputType);

            if (Instance.UIControl.CurrentIndex == 0)
            {
                Instance.SetUIControl(inputIndex);
            }

            return true;
        }

        /// <summary>
        /// 重置清單的數量
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<IInputList> Resize(int count) 
        {
            return Instance.Resize(count);
        }

        /// <summary>
        /// 取得輸入清單
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public static IInputList FetchList(int Index, EInputType inputType = EInputType.KeyBoard) 
        {
            return Instance.FetchList(Index, inputType);
        }

        /// <summary>
        /// 更改指定輸入
        /// </summary>
        /// <param name="listIndex"></param>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public static async Task<bool> ChangeInput(int listIndex, int uuid) 
        {
            return await Instance.ChangeInput(listIndex, uuid);
        }

        #endregion
    }
}
