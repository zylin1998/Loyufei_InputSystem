using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Loyufei.InputSystem
{
    public partial class InputManager : MonoBehaviour
    {
        [SerializeField]
        private List<AxisPair>  _Axis       = new();
        [SerializeField]
        private UIControlInput  _UIControl;
        [SerializeField]
        private List<InputList> _InputLists = new();

        internal Dictionary<int, IInput> Inputs { get; } = new();

        private void Awake()
        {
            var inputModule = EventSystem.current?.GetComponent<BaseInputModule>();

            if (inputModule)
            {
                _UIControl = inputModule.gameObject.AddComponent<UIControlInput>();
                
                inputModule.inputOverride = _UIControl;
            }
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
        /// 取得或新增輸入
        /// </summary>
        /// <param name="index"></param>
        internal IInput GetInput(int index) 
        {
            var exist = Inputs.TryGetValue(index, out var input);

            if (!exist)
            {
                input = new InputBase();

                if (input is IInputBinder binder) { binder.Binding(_Axis, GetList(index)); }

                Inputs.TryAdd(index, input);
            }

            return input;
        } 

        /// <summary>
        /// 取得輸入清單
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        internal InputList GetList(int index) 
        {
            var list = _InputLists.Find(l => l.Index == index);

            if (list) { return list; }

            list = new GameObject("InputList" + index).AddComponent<InputList>();

            list.SetIndex(index);
            list.Init(DefaultInputs);

            list.transform.SetParent(transform);

            _InputLists.Add(list);

            return list;
        }
    }
}
