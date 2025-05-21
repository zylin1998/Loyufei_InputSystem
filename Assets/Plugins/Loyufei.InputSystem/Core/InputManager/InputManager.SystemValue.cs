using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Loyufei.InputSystem
{
    public static partial class InputManager
    {
        #region Private Static Field

        private static InternalTaskDispatcher _Instance;

        #endregion

        #region Static Properties

        /// <summary>
        /// InputManager 單例實作
        /// </summary>
        internal static InternalTaskDispatcher Instance 
        {
            get 
            {
                if (!_Instance) 
                {
                    _Instance = new GameObject("InputManager").AddComponent<InternalTaskDispatcher>();

                    GameObject.DontDestroyOnLoad(_Instance);
                }

                return _Instance;
            } 
        }

        /// <summary>
        /// 輸入更換重疊時動作選擇
        /// </summary>
        public static ESameEncounter SameEncounter
        {
            get => Instance.SameEncounter; 
            
            set => Instance.SameEncounter = value; 
        }

        public static KeyCode[] KeyCodes { get; } 
            = Enum.GetValues(typeof(EInputCode)).Cast<KeyCode>().ToArray();

        public static KeyCode[] IgnoreKeyCode { get; set; } = new KeyCode[0];

        public static KeyCode[] AvailableKeyCode => KeyCodes.Except(IgnoreKeyCode).ToArray();

        #endregion
    }
}