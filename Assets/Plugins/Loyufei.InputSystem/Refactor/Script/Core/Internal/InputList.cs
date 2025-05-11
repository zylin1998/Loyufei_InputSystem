using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loyufei.InputSystem
{
    internal class InputList : MonoBehaviour, IInputList, IInputBindings
    {
        #region Fields

        [SerializeField]
        private int               _Index;
        [SerializeField]
        private EInputType        _InputType;
        [SerializeField]
        private List<BindingPair> _Bindings = new();

        #endregion

        #region Properties

        /// <summary>
        /// 輸入清單索引
        /// </summary>
        public int        Index     => _Index;

        /// <summary>
        /// 顯示該清單服務的輸入平台。
        /// </summary>
        public EInputType InputType => _InputType;

        #endregion

        #region Public Methods

        /// <summary>
        /// 設置輸入的 Index 順序
        /// </summary>
        /// <param name="index"></param>
        public void SetIndex(int index) 
        {
            _Index = index;
        }

        /// <summary>
        /// 更改輸入平台。
        /// </summary>
        /// <param name="type"></param>
        public void SetInputType(EInputType type) 
        {
            _InputType = type;
        }

        #endregion

        #region IInputList

        public void Init(IInputList inputList)
        {
            _InputType = inputList.InputType;

            foreach (var pair in inputList.GetPairs()) 
            {
                var binding = _Bindings.Find(b => b.UUID == pair.UUID);

                if (binding == null)
                {
                    binding = new(pair);

                    _Bindings.Add(binding);
                }

                else 
                {
                    binding.Reset(pair.InputCode);
                }
            }
        }

        public IEnumerable<InputPair> GetPairs()
        {
            return _Bindings.Select(b => new InputPair(b.UUID, b.InputCode));
        }

        #endregion

        #region IInputBindings

        public BindingPair this[int uuid]
        {
            get
            {
                TryGet(uuid, out var bindings);

                return bindings;
            }
        }

        public BindingPair this[EInputCode inputCode]
        {
            get
            {
                TryGet(inputCode, out var bindings);

                return bindings;
            }
        }

        public bool TryGet(int uuid, out BindingPair value)
        {
            value = _Bindings.SingleOrDefault(p => p.UUID == uuid) ?? new(0, EInputCode.None);

            return value.UUID == uuid;
        }

        public bool TryGet(EInputCode inputCode, out BindingPair value)
        {
            value = _Bindings.SingleOrDefault(p => p.InputCode == inputCode) ?? new(0, EInputCode.None);

            return value.InputCode == inputCode;
        }

        public InputRebindResult Rebinding(int uuid, EInputCode inputCode, ESameEncounter onSame)
        {
            var exist = TryGet(uuid, out var binding);

            if (exist)
            {
                return InputRebindStrategy.GetRebinder(onSame).Rebind(this, uuid, inputCode);
            }

            return new(false, Index, InputType, BindingPair.Default, BindingPair.Default);
        }

        #endregion
    }
}
