using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace InputSystem
{
    [Serializable]
    internal class BindingPair
    {
        #region Fields

        [SerializeField]
        private int     _UUID;
        [SerializeField]
        private KeyCode _KeyCode;

        #endregion

        #region Properties

        /// <summary>
        /// 通用唯一辨識碼
        /// </summary>
        public int     UUID    => _UUID;
        /// <summary>
        /// Unity輸入
        /// </summary>
        public KeyCode KeyCode => _KeyCode;

        #endregion

        #region Constructor

        public BindingPair(int uuid, KeyCode keyCode)
        {
            Reset(uuid, keyCode);
        }

        public BindingPair(InputPair pair) 
        {
            Reset(pair.UUID, pair.KeyCode);
        }

        #endregion

        #region Public Methods

        public void Reset(int uuid, KeyCode keyCode) 
        {
            _UUID    = uuid;
            _KeyCode = keyCode;
        }

        public void Reset(KeyCode keyCode)
        {
            _KeyCode = keyCode;
        }

        #endregion
    }
}
