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
        private int _Index;
        [SerializeField]
        private List<BindingPair> _Bindings = new();

        #endregion

        #region Properties

        /// <summary>
        /// 標示輸入的順序
        /// </summary>
        public int Index => _Index;

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
        /// 設置輸入的 Index 順序
        /// </summary>
        /// <param name="index"></param>
        public void SetIndex(int index) 
        {
            _Index = index;
        }

        /// <summary>
        /// 新增Binding
        /// </summary>
        /// <param name="pair"></param>
        /// <returns></returns>
        public bool Add(InputPair pair)
        {
            var exist = _Bindings.Exists(b => b.UUID == pair.UUID);

            if (exist) { return true; }

            _Bindings.Add(new(pair));

            return true;
        }

        /// <summary>
        /// 移除與輸入UUID相符的輸入
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public bool Remove(int uuid)
        {
            var exist = _Bindings.Exists(b => b.UUID == uuid);

            if (!exist) { return false; }

            return _Bindings.Remove(_Bindings.Find(b => b.UUID == uuid));
        }

        /// <summary>
        /// 更換輸入並檢查重複
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="keyCode"></param>
        /// <param name="onSame"></param>
        /// <returns></returns>
        public bool ChangeInput(int uuid, KeyCode keyCode, ESameEncounter onSame)
        {
            var exist = TryGet(uuid, out var binding);

            if (exist)
            {
                return Strategy[onSame].Invoke(binding, keyCode);
            }

            return exist;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 輸入檢查策略
        /// </summary>
        private Dictionary<ESameEncounter, Func<BindingPair, KeyCode, bool>> Strategy;

        /// <summary>
        /// 若輸入重複則回傳失敗
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        private bool None(BindingPair binding, KeyCode keyCode)
        {
            var same = _Bindings.Find(b => b.KeyCode == keyCode);

            if (same != null) { return false; }

            binding.Reset(keyCode);

            return true;
        }

        /// <summary>
        /// 若輸入重複則刪除重複
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        private bool Delete(BindingPair binding, KeyCode keyCode) 
        {
            var same = _Bindings.Find(b => b.KeyCode == keyCode);

            if (same != null) { same.Reset(KeyCode.None); }

            binding.Reset(keyCode);

            return true;
        }

        /// <summary>
        /// 若輸入重複則交換
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        private bool Exchange(BindingPair binding, KeyCode keyCode)
        {
            var same = _Bindings.Find(b => b.KeyCode == keyCode);

            if (same != null) { same.Reset(binding.KeyCode); }

            binding.Reset(keyCode);

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
            return _Bindings.Select(b => new InputPair(b.UUID, b.KeyCode));
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
            value = _Bindings.SingleOrDefault(p => p.UUID == uuid) ?? new(0, KeyCode.None);

            return value != null;
        }

        #endregion
    }
}
