using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Loyufei.InputSystem
{
    [Serializable]
    public class BindingPair
    {
        #region Fields

        [SerializeField]
        private int _UUID;
        [SerializeField]
        private EInputCode _InputCode;

        #endregion

        #region Properties

        /// <summary>
        /// 通用唯一辨識碼
        /// </summary>
        public int UUID => _UUID;
        /// <summary>
        /// Unity InputSystem 和 XInputDonet 橋接輸入
        /// </summary>
        public EInputCode InputCode => _InputCode;

        #endregion

        #region Constructor

        public BindingPair(int uuid, EInputCode keyCode)
        {
            Reset(uuid, keyCode);
        }

        public BindingPair(InputPair pair)
        {
            Reset(pair.UUID, pair.InputCode);
        }

        #endregion

        #region Public Methods

        public void Reset(int uuid, EInputCode inputCode)
        {
            _UUID = uuid;
            _InputCode = inputCode;
        }

        public void Reset(EInputCode inputCode)
        {
            _InputCode = inputCode;
        }

        #endregion

        #region Static Value

        public static BindingPair Default { get; } = new(0, EInputCode.None);

        #endregion
    }
}
