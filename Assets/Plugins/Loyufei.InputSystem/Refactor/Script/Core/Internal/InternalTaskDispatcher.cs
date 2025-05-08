using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    internal class InternalTaskDispatcher : MonoBehaviour
    {
        #region Const Field

        public const int minIndex = 1;
        public const int maxIndex = 4;

        #endregion

        [SerializeField, Range(minIndex, maxIndex)]
        private int             _IndexCount = maxIndex;
        [SerializeField]
        private ESameEncounter  _SameEncounter = ESameEncounter.None;
        [SerializeField]
        private List<AxisPair>  _Axis = new();
        [SerializeField]
        private UIControlInput  _UIControl;
        [SerializeField]
        private List<InputList> _InputLists = new();

        #region Internal Properties

        internal UIControlInput UIControl => _UIControl;

        internal Dictionary<int, IInput> Inputs { get; } = new();

        internal int IndexCount
        {
            get => _IndexCount;

            set
            {
                if (_IndexCount == value) { return; }

                _IndexCount = value > maxIndex ? maxIndex : value;
                _IndexCount = value < minIndex ? minIndex : value;
            }
        }

        internal ESameEncounter SameEncounter 
        {
            get => _SameEncounter; 
            
            set => _SameEncounter = value; 
        }

        internal Dictionary<EInputType, IInputList> DefaultInputLists { get; } = new()
        {
            { EInputType.KeyBoard      , IInputList.Default },
            { EInputType.GameController, IInputList.Default },
        };

        #endregion

        #region Unity Behaviour

        private void Awake()
        {
            var inputModule = EventSystem.current?.GetComponent<BaseInputModule>();

            if (inputModule)
            {
                _UIControl = inputModule.gameObject.AddComponent<UIControlInput>();
                
                inputModule.inputOverride = _UIControl;
            }
        }

        #endregion

        #region Internal Method

        /// <summary>
        /// 判斷輸入索引是否有效
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsIndexValid(int index)
        {
            if (index < minIndex) { return false; }
            if (index > maxIndex) { return false; }

            return true;
        }

        /// <summary>
        /// 設置UI控制輸入需要的輸入索引
        /// </summary>
        /// <param name="index"></param>
        internal void SetUIControl(int index) 
        {
            _UIControl.SetIndex(index, Inputs[index]);
        }

        /// <summary>
        /// 設置輸入軸資訊
        /// </summary>
        /// <param name="inputAxis"></param>
        internal void SetAxis(IInputAxis inputAxis) 
        {
            _Axis = inputAxis.GetPairs().ToList();
        }

        /// <summary>
        /// 設置初始輸入清單
        /// </summary>
        /// <param name="inputList"></param>
        /// <param name="inputType"></param>
        internal void SetList(IInputList inputList, EInputType inputType) 
        {
            DefaultInputLists[inputType] = inputList;
        }

        /// <summary>
        /// 取得或新增輸入
        /// </summary>
        /// <param name="index"></param>
        /// <param name="inputBindings"></param>
        /// <returns></returns>
        internal IInput FetchInput(int index, IInputBindings inputBindings, EInputType inputType)
        {
            var exist = Inputs.TryGetValue(index, out var input);

            if (!exist)
            {
                var constructor = AxisConstructStrategy.GetConstructor(inputType);

                input = new InputBase(constructor, inputType, index);

                if (input is IInputBinder binder) { binder.Binding(_Axis, inputBindings); }

                Inputs.TryAdd(index, input);
            }

            return input;
        }

        /// <summary>
        /// 取得輸入清單
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        internal InputList FetchList(int index, EInputType inputType) 
        {
            var list = _InputLists.Find(l => l.Index == index);

            if (list) { return list; }

            list = new GameObject("InputList" + index).AddComponent<InputList>();

            list.SetIndex(index);
            list.SetInputType(inputType);
            list.Init(DefaultInputLists[inputType]);

            list.transform.SetParent(transform);

            _InputLists.Add(list);

            return list;
        }

        /// <summary>
        /// 切換輸入頻道的綁定清單
        /// </summary>
        /// <param name="input"></param>
        /// <param name="listIndex"></param>
        /// <returns></returns>
        internal bool SwitchList(IInput input, int listIndex) 
        {
            if (input is IInputBinder binder) 
            {
                var list = FetchList(listIndex, input.InputType);

                binder.Binding(_Axis, list);

                return true;
            }

            return false;
        }

        /// <summary>
        /// 重置清單的數量
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        internal IEnumerable<IInputList> Resize(int size) 
        {
            return (_InputLists = SizeCheck(size).ToList());
        }
        
        /// <summary>
        /// 更改指定輸入
        /// </summary>
        /// <param name="listIndex"></param>
        /// <param name="uuid"></param>
        /// <returns></returns>
        internal async Task<bool> ChangeInput(IInput input, int uuid) 
        {
            if (input is IInputBinder binder) 
            {
                var code = await AwaitInputCodeDown(input);

                var list = binder.Bindings;
                
                return list.Rebinding(uuid, code, SameEncounter);
            }

            return false;
        }

        /// <summary>
        /// 取得當前所有輸入清單的資訊
        /// </summary>
        /// <returns></returns>
        internal InputPair[][] GetAllList() 
        {
            return _InputLists
                .OrderBy(key => key.InputType)
                .Select(list => list.GetPairs().ToArray())
                .ToArray();
        }

        #endregion

        #region Private Method

        /// <summary>
        /// 整理清單數量
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        internal IEnumerable<InputList> SizeCheck(int size)
        {
            var index = 0;

            foreach (var input in _InputLists) 
            {
                if (input.Index <= size) 
                {
                    yield return input;

                    index++;
                }

                else 
                {
                    Destroy(input.gameObject); 
                }
            }
        }

        /// <summary>
        /// 異步等待按鍵被按下
        /// </summary>
        /// <returns></returns>
        private async Task<EInputCode> AwaitInputCodeDown(IInput input) 
        {   
            var checker = InputCodeCheckStrategy.GetChecker(input.InputType);
            
            var code = EInputCode.None;

            for(; code == EInputCode.None;) 
            {
                code = checker.Check(input.Index);

                await Task.Yield();
            }

            return code;
        }

        #endregion
    }
}
