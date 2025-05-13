using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Loyufei.InputSystem
{
    internal class UIControlInput : BaseInput
    {
        [SerializeField]
        private int        _CurrentIndex;
        [SerializeField]
        private EInputType _CurrentInputType;
        
        /// <summary>
        /// 當前輸入頻道索引
        /// </summary>
        public int CurrentIndex => _CurrentIndex;

        /// <summary>
        /// 當前輸入頻道對應的平台。
        /// </summary>
        public EInputType CurrentInputType => _CurrentInputType;

        /// <summary>
        /// 當前輸入頻道
        /// </summary>
        public IInput Input { get; private set; } = IInput.Default;

        public void SetIndex(IInput input) 
        {
            Input         = input;

            _CurrentIndex     = Input.Index;
            _CurrentInputType = Input.InputType;
        }

        public override float GetAxisRaw(string axisName)
        {
            return Input.GetAxisRaw(axisName);
        }

        public override bool GetButtonDown(string buttonName)
        {
            return Input.GetKeyDown(buttonName);
        }
    }
}
