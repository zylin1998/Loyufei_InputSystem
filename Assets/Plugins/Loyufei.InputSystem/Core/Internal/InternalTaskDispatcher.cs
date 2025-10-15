using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Loyufei.InputSystem
{
    internal class InternalTaskDispatcher : MonoBehaviour
    {
        #region Const Field

        public const int minIndex = 1;
        public const int maxIndex = 4;

        #endregion

        #region Fields

        [SerializeField]
        private ESameEncount   _SameEncount = ESameEncount.None;
        [SerializeField]
        private UIControlInput   _UIControl;
        [SerializeField]
        private List<InputLayer> _Layers = new List<InputLayer>();
        
        private readonly CancellationTokenSource _TokenSource = new();

        #endregion

        #region Internal Properties

        internal UIControlInput UIControl => _UIControl;

        internal ESameEncount SameEncount 
        {
            get => _SameEncount; 
            
            set => _SameEncount = value; 
        }

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

        private void OnDestroy()
        {
            _TokenSource.Cancel();
        }

        #endregion

        #region Internal Method

        internal void SetIndexCount(int indexCount, int layer)
        {
            if (!IsLayerValid(layer, out var inputLayer))
            {
                return;
            }

            inputLayer.IndexCount = indexCount;
        }

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
        internal void SetUIControl(int index, int layer) 
        {
            if (!IsLayerValid(layer, out var inputLayer)) 
            {
                return; 
            }
            
            _UIControl.SetIndex(inputLayer.Inputs[index]);
        }

        /// <summary>
        /// 設置輸入軸資訊
        /// </summary>
        /// <param name="inputAxis"></param>
        internal void SetAxis(IInputAxis inputAxis, int layer) 
        {
            if (!IsLayerValid(layer, out var inputLayer, true))
            {
                return;
            }

            inputLayer.SetAxis(inputAxis);
        }

        /// <summary>
        /// 設置初始輸入清單
        /// </summary>
        /// <param name="inputList"></param>
        /// <param name="inputType"></param>
        internal void SetList(IInputList inputList, int layer) 
        {
            if (!IsLayerValid(layer, out var inputLayer, true))
            {
                return;
            }
            
            inputLayer.DefaultInputLists[inputList.InputType] = inputList;
        }

        /// <summary>
        /// 重製索引及服務平台相對應的輸入清單
        /// </summary>
        /// <param name="index"></param>
        /// <param name="inputType"></param>
        /// <returns></returns>
        internal IEnumerable<InputPair> ResetList(int index, EInputType type, int layer) 
        {
            if (!IsLayerValid(layer, out var inputLayer))
            {
                return IInputList.Default.GetPairs();
            }

            var list = FetchList(index, type, layer);

            list.Init(inputLayer.DefaultInputLists[type]);

            return list.GetPairs();
        }

        /// <summary>
        /// 取得或新增輸入
        /// </summary>
        /// <param name="index"></param>
        /// <param name="inputBindings"></param>
        /// <returns></returns>
        internal IInput FetchInput(int index, IInputBindings inputBindings, EInputType inputType, int layer)
        {
            if (!IsLayerValid(layer, out var inputLayer))
            {
                return IInput.Default;
            }
            
            return inputLayer.GetInput(index, inputBindings, inputType);
        }

        /// <summary>
        /// 取得輸入清單
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        internal InputList FetchList(int index, EInputType inputType, int layer) 
        {
            if (!IsLayerValid(layer, out var inputLayer))
            {
                return default;
            }

            return inputLayer.GetList(index, inputType);
        }

        /// <summary>
        /// 切換輸入頻道的綁定清單
        /// </summary>
        /// <param name="input"></param>
        /// <param name="listIndex"></param>
        /// <returns></returns>
        internal bool SwitchList(IInput input, int listIndex, int layer) 
        {
            if (!IsLayerValid(layer, out var inputLayer))
            {
                return false;
            }

            return inputLayer.SwitchList(input, listIndex);
        }

        /// <summary>
        /// 重置清單的數量
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        internal IEnumerable<IInputList> Resize(int size, int layer) 
        {
            if (!IsLayerValid(layer, out var inputLayer))
            {
                return new IInputList[0];
            }

            return inputLayer.Resize(size);
        }
        
        /// <summary>
        /// 更改指定輸入
        /// </summary>
        /// <param name="listIndex"></param>
        /// <param name="uuid"></param>
        /// <returns></returns>
        internal async Task<InputRebindResult> Rebind(IInput input, int uuid) 
        {
            if (input is IInputBinder binder) 
            {
                var code = await AwaitInputCodeDown(input);

                var list = binder.Bindings;
                
                return list.Rebinding(uuid, code, SameEncount);
            }

            return new(false, 0, input.InputType, BindingPair.Default, BindingPair.Default);
        }

        /// <summary>
        /// 取得當前所有輸入清單的資訊
        /// </summary>
        /// <returns></returns>
        internal InputPackage GetAllList(int layer) 
        {
            if (!IsLayerValid(layer, out var inputLayer))
            {
                return new();
            }

            var list = new List<IInputList>();

            for (int i = minIndex; i <= maxIndex; i++) 
            {
                list.Add(FetchList(i, EInputType.KeyBoard, layer));
            }

            for (int i = minIndex; i <= maxIndex; i++)
            {
                list.Add(FetchList(i, EInputType.GameController, layer));
            }

            return new(list);
        }

        /// <summary>
        /// 取得當前所有輸入清單的資訊
        /// </summary>
        /// <returns></returns>
        internal InputPackage GetAllList(EInputType inputType, int layer)
        {
            var list = new List<IInputList>();

            for (int i = minIndex; i <= maxIndex; i++)
            {
                list.Add(FetchList(i, inputType, layer));
            }

            return new(list);
        }

        #endregion

        #region Private Method

        /// <summary>
        /// 異步等待按鍵被按下
        /// </summary>
        /// <returns></returns>
        private async Task<EInputCode> AwaitInputCodeDown(IInput input) 
        {   
            var checker = InputCodeCheckStrategy.GetChecker(input.InputType);
            
            var code = EInputCode.None;

            for (; input.AnyKey();) 
            {
                await Task.Yield();
            }

            for (; code == EInputCode.None && !_TokenSource.Token.IsCancellationRequested;) 
            {
                await Task.Yield();
                
                code = checker.Check(input.Index);
            }

            return code;
        }

        /// <summary>
        /// 確認階層是否存在
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="inputLayer"></param>
        /// <param name="createInstance"></param>
        /// <returns></returns>
        private bool IsLayerValid(int layer, out InputLayer inputLayer, bool createInstance = false) 
        {
            inputLayer = _Layers.Find(inputLayer => inputLayer.Layer == layer);

            if (inputLayer == null) 
            {
                if(!createInstance) 
                {
                    Debug.LogError("無效輸入階層編號");

                    return false; 
                }

                inputLayer = new(layer, transform);

                _Layers.Add(inputLayer);
            }

            return true;
        }

        #endregion

        [Serializable]
        internal class InputLayer 
        {
            #region Constructor

            public InputLayer(int layer, Transform root) 
            {
                _Layer = layer;
                _Root  = root;
            }

            #endregion

            #region Fields

            [SerializeField]
            private int             _Layer;
            [SerializeField, Range(minIndex, maxIndex)]
            private int             _IndexCount = maxIndex;
            [SerializeField]
            private List<AxisPair>  _Axis = new();
            [SerializeField]
            private List<InputList> _InputLists = new();

            private Transform _Root;

            #endregion

            #region Property

            internal int Layer => _Layer;

            internal Dictionary<int, IInput> Inputs { get; } = new();

            internal Dictionary<EInputType, IInputList> DefaultInputLists { get; } = new()
            {
                { EInputType.KeyBoard      , IInputList.Default },
                { EInputType.GameController, IInputList.Default },
            };

            internal int IndexCount
            {
                get => _IndexCount;

                set => _IndexCount = value ;
            }

            #endregion

            #region Public Method

            public void SetAxis(IInputAxis axis) 
            {
                _Axis = axis.GetPairs().ToList();
            }

            public IInput GetInput(int index, IInputBindings inputBindings, EInputType inputType) 
            {
                if (index > IndexCount) 
                {
                    return IInput.Default;
                }

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

            public InputList GetList(int index, EInputType inputType) 
            {
                var list = _InputLists.Find(l => l.Index == index && l.InputType == inputType);

                if (list) { return list; }

                list = new GameObject("InputList" + index).AddComponent<InputList>();

                list.SetIndex(index);
                list.SetInputType(inputType);
                list.Init(DefaultInputLists[inputType]);

                list.transform.SetParent(_Root);

                _InputLists.Add(list);

                return list;
            }

            public bool SwitchList(IInput input, int listIndex) 
            {
                if (!Inputs.Values.Contains(input)) { return false; }

                if (input is IInputBinder binder)
                {
                    var list = GetList(listIndex, input.InputType);

                    binder.Binding(_Axis, list);

                    return true;
                }

                return false;
            }

            public IEnumerable<IInputList> Resize(int size) 
            {
                return (_InputLists = SizeCheck(size).ToList());
            }

            #endregion

            #region Private Method

            /// <summary>
            /// 整理清單數量
            /// </summary>
            /// <param name="size"></param>
            /// <returns></returns>
            private IEnumerable<InputList> SizeCheck(int size)
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

            #endregion
        }
    }
}
