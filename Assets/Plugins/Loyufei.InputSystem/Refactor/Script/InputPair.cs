using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace InputSystem
{
    [Serializable]
    public struct InputPair
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

        public InputPair(int uuid, KeyCode keyCode)
        {
            _UUID = uuid;
            _KeyCode = keyCode;
        }

        #endregion
    }
}
