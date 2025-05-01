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
        private int _CurrentIndex;
        
        /// <summary>
        /// 當前輸入頻道索引
        /// </summary>
        public int CurrentIndex => _CurrentIndex;

        /// <summary>
        /// 當前輸入頻道
        /// </summary>
        public IInput Input { get; private set; }

        /// <summary>
        /// 當前輸入頻道是否有效
        /// </summary>
        public bool IsInputValid => Input != null && Input != IInput.Default;

        public void SetIndex(int index, IInput input) 
        {
            _CurrentIndex = index;
            Input         = input;
        }

        public override float GetAxisRaw(string axisName)
        {
            return IsInputValid ? Input.GetAxisRaw(axisName) : base.GetAxisRaw(axisName);
        }

        public override bool GetButtonDown(string buttonName)
        {
            return IsInputValid ? Input.GetKeyDown(buttonName) : base.GetButtonDown(buttonName);
        }
    }
}
