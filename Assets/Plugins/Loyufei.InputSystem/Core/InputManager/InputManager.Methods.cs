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
        /// 切換UI控制的輸入頻道
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
        public static void SetDefault(IInputAxis inputAxis) 
        {
            Instance.SetAxis(inputAxis);
        }

        /// <summary>
        /// 設定預設輸入清單資訊
        /// </summary>
        /// <param name="inputList"></param>
        public static void SetDefault(IInputList inputList) 
        {
            Instance.SetList(inputList);
        }

        /// <summary>
        /// 以索引取得輸入頻道
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static IInput FetchInput(int index, EInputType inputType) 
        {
            return FetchInput(index, index, inputType);
        }

        /// <summary>
        /// 以索引取得輸入頻道
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static IInput FetchInput(int inputIndex, int listIndex, EInputType inputType)
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
        /// 取得輸入清單
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public static IInputList FetchList(int Index, EInputType inputType) 
        {
            return Instance.FetchList(Index, inputType);
        }

        /// <summary>
        /// 取得所有輸入清單資訊
        /// </summary>
        /// <returns></returns>
        public static InputPackage FetchLists() 
        {
            return Instance.GetAllList();
        }

        /// <summary>
        /// 取得特定輸入平台的所有輸入清單資訊
        /// </summary>
        /// <returns></returns>
        public static InputPackage FetchLists(EInputType inputType)
        {
            return Instance.GetAllList(inputType);
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
        /// 更改指定輸入
        /// </summary>
        /// <param name="listIndex"></param>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public static async Task<InputRebindResult> Rebind(IInput self, int uuid)
        {
            return await Instance.Rebind(self, uuid);
        }

        /// <summary>
        /// 切換輸入頻道的綁定清單
        /// </summary>
        /// <param name="self"></param>
        /// <param name="listIndex"></param>
        /// <returns></returns>
        public static bool SwitchList(IInput self, int listIndex) 
        {
            return Instance.SwitchList(self, listIndex);
        }

        /// <summary>
        /// 重製索引及服務平台相對應的輸入清單
        /// </summary>
        /// <param name="index"></param>
        /// <param name="inputType"></param>
        /// <returns></returns>
        public static IEnumerable<InputPair> ResetList(int index, EInputType inputType) 
        {
            return Instance.ResetList(index, inputType);
        }

        #region Input Management Strategy

        /// <summary>
        /// 新增輸入軸建構策略
        /// </summary>
        /// <param name="key"></param>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static bool AddStrategy(object key, IAxisConstructor strategy) 
        {
            return AxisConstructStrategy.AddStrategy(key, strategy);
        }

        /// <summary>
        /// 新增輸入檢測策略
        /// </summary>
        /// <param name="key"></param>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static bool AddStrategy(object key, IInputCodeChecker strategy)
        {
            return InputCodeCheckStrategy.AddStrategy(key, strategy);
        }

        /// <summary>
        /// 新增輸入重新綁定策略
        /// </summary>
        /// <param name="key"></param>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static bool AddStrategy(object key, IInputRebinder strategy)
        {
            return InputRebindStrategy.AddStrategy(key, strategy);
        }

        /// <summary>
        /// 以委派方式新增輸入軸建構策略
        /// </summary>
        /// <param name="key"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static bool AddStrategy(object key, Func<AxisPair, IInputBindings, IAxis> callback) 
        {
            return AddStrategy(key, new CallbackConstructor(callback));
        }

        /// <summary>
        /// 以委派方式新增輸入檢測策略
        /// </summary>
        /// <param name="key"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static bool AddStrategy(object key, Func<int, EInputCode> callback)
        {
            return AddStrategy(key, new CallbackChecker(callback));
        }

        /// <summary>
        /// 以委派方式新增輸入重新綁定策略
        /// </summary>
        /// <param name="key"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static bool AddStrategy(object key, Func<IInputBindings, int, EInputCode, InputRebindResult> callback)
        {
            return AddStrategy(key, new CallbackRebinder(callback));
        }

        #endregion

        #endregion
    }
}
