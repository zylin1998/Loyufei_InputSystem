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
        /// ��J�M�����
        /// </summary>
        public int        Index     => _Index;

        /// <summary>
        /// ��ܸӲM��A�Ȫ���J���x�C
        /// </summary>
        public EInputType InputType => _InputType;

        #endregion

        #region Constructor

        internal InputList() : base()
        {
            Strategy = new()
            {
                { ESameEncounter.None    , None     },
                { ESameEncounter.Delete  , Delete   },
                { ESameEncounter.Exchange, Exchange },
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// �]�m��J�� Index ����
        /// </summary>
        /// <param name="index"></param>
        public void SetIndex(int index) 
        {
            _Index = index;
        }

        /// <summary>
        /// ����J���x�C
        /// </summary>
        /// <param name="type"></param>
        public void SetInputType(EInputType type) 
        {
            _InputType = type;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// ��J�ˬd����
        /// </summary>
        private Dictionary<ESameEncounter, Func<BindingPair, EInputCode, bool>> Strategy;

        /// <summary>
        /// �Y��J���ƫh�^�ǥ���
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        private bool None(BindingPair binding, EInputCode inputCode)
        {
            var same = _Bindings.Find(b => b.InputCode == inputCode);

            if (same != null) { return false; }

            binding.Reset(inputCode);

            return true;
        }

        /// <summary>
        /// �Y��J���ƫh�R������
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        private bool Delete(BindingPair binding, EInputCode inputCode) 
        {
            var same = _Bindings.Find(b => b.InputCode == inputCode);

            if (same != null) { same.Reset(EInputCode.None); }

            binding.Reset(inputCode);

            return true;
        }

        /// <summary>
        /// �Y��J���ƫh�洫
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        private bool Exchange(BindingPair binding, EInputCode inputCode)
        {
            var same = _Bindings.Find(b => b.InputCode == inputCode);

            if (same != null) { same.Reset(binding.InputCode); }

            binding.Reset(inputCode);

            return true;
        }

        #endregion

        #region IInputList

        public void Init(IInputList inputList)
        {
            _Bindings = inputList
                .GetPairs()
                .Select(p => new BindingPair(p)) 
                .ToList();
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

        public bool TryGet(int uuid, out BindingPair value)
        {
            value = _Bindings.SingleOrDefault(p => p.UUID == uuid) ?? new(0, EInputCode.None);

            return value.UUID == uuid;
        }

        /// <summary>
        /// �󴫿�J���ˬd����
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="keyCode"></param>
        /// <param name="onSame"></param>
        /// <returns></returns>
        public bool Rebinding(int uuid, EInputCode inputCode, ESameEncounter onSame)
        {
            var exist = TryGet(uuid, out var binding);

            if (exist)
            {
                return Strategy[onSame].Invoke(binding, inputCode);
            }

            return exist;
        }

        #endregion
    }
}
