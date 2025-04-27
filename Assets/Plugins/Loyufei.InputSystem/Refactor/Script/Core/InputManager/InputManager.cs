using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Loyufei.InputSystem
{
    public partial class InputManager : MonoBehaviour
    {
        [SerializeField]
        private List<InputList> _InputLists = new();
        [SerializeField]
        private List<AxisPair>  _Axis       = new();

        internal Dictionary<int, IInput> Inputs { get; } = new();

        public void SetAxis(IInputAxis inputAxis) 
        {
            _Axis = inputAxis.GetPairs().ToList();
        }

        /// <summary>
        /// 取得或新增輸入
        /// </summary>
        /// <param name="index"></param>
        public IInput GetInput(int index) 
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
