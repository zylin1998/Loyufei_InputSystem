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
        /// 設置輸入頻道上限
        /// </summary>
        /// <param name="indexCount"></param>
        /// <param name="layer"></param>
        public static void SetIndexCount(int indexCount, int layer = 0) 
        {
            if (!Instance.IsIndexValid(indexCount)) { return; }

            Instance.SetIndexCount(indexCount, layer);
        }

        /// <summary>
        /// 切換UI控制的輸入頻道
        /// </summary>
        /// <param name="index"></param>
        public static void SwitchUIControlInput(int index, int layer = 0)
        {
            if (!Instance.IsIndexValid(index)) { return; }

            Instance.SetUIControl(index, layer);
        }

        /// <summary>
        /// 設定預設輸入軸資訊
        /// </summary>
        /// <param name="inputAxis"></param>
        public static void SetDefault(IInputAxis inputAxis, int layer = 0) 
        {
            Instance.SetAxis(inputAxis, layer);
        }

        /// <summary>
        /// 設定預設輸入清單資訊
        /// </summary>
        /// <param name="inputList"></param>
        public static void SetDefault(IInputList inputList, int layer = 0) 
        {
            Instance.SetList(inputList, layer);
        }

        /// <summary>
        /// 以索引取得輸入頻道
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static IInput FetchInput(int index, EInputType inputType, int layer = 0) 
        {
            return FetchInput(index, index, inputType, layer);
        }

        /// <summary>
        /// 以索引取得輸入頻道
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static IInput FetchInput(int inputIndex, int listIndex, EInputType inputType, int layer = 0)
        {
            if (!Instance.IsIndexValid(inputIndex))
            {
                return IInput.Default;
            }

            var list  = Instance.FetchList(listIndex, inputType, layer);
            var input = Instance.FetchInput(inputIndex, list, inputType, layer);

            if (Instance.UIControl.CurrentIndex == 0)
            {
                SwitchUIControlInput(inputIndex, layer);
            }

            return input;
        }

        /// <summary>
        /// 嘗試以索引取得輸入頻道
        /// </summary>
        /// <param name="index"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool TryFetchInput(int index, EInputType inputType, out IInput input, int layer = 0) 
        {
            return TryFetchInput(index, index, inputType, out input, layer);
        }

        /// <summary>
        /// 嘗試以索引取得輸入頻道
        /// </summary>
        /// <param name="index"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool TryFetchInput(int inputIndex, int listIndex, EInputType inputType, out IInput input, int layer = 0)
        {
            if (!Instance.IsIndexValid(inputIndex))
            {
                input = IInput.Default;

                return false;
            }

            var list = Instance.FetchList(listIndex, inputType, layer);

            input = Instance.FetchInput(inputIndex, list, inputType, layer);

            if (Instance.UIControl.CurrentIndex == 0)
            {
                Instance.SetUIControl(inputIndex, layer);
            }

            return true;
        }

        /// <summary>
        /// 取得輸入清單
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public static IInputList FetchList(int Index, EInputType inputType, int layer = 0) 
        {
            return Instance.FetchList(Index, inputType, layer);
        }

        /// <summary>
        /// 取得所有輸入清單資訊
        /// </summary>
        /// <returns></returns>
        public static InputPackage FetchLists(int layer = 0) 
        {
            return Instance.GetAllList(layer);
        }

        /// <summary>
        /// 取得特定輸入平台的所有輸入清單資訊
        /// </summary>
        /// <returns></returns>
        public static InputPackage FetchLists(EInputType inputType, int layer = 0)
        {
            return Instance.GetAllList(inputType, layer);
        }

        /// <summary>
        /// 重置清單的數量
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<IInputList> Resize(int count, int layer = 0)
        {
            return Instance.Resize(count, layer);
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
        public static bool SwitchList(IInput self, int listIndex, int layer = 0) 
        {
            return Instance.SwitchList(self, listIndex, layer);
        }

        /// <summary>
        /// 重製索引及服務平台相對應的輸入清單
        /// </summary>
        /// <param name="index"></param>
        /// <param name="inputType"></param>
        /// <returns></returns>
        public static IEnumerable<InputPair> ResetList(int index, EInputType inputType, int layer = 0)
        {
            return Instance.ResetList(index, inputType, layer);
        }
        ///多元功能尚未規畫完成，暫時註解起來。
        /* 
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
        */
        #endregion
    }
}
