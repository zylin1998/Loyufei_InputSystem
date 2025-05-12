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
        /// 自訂輸入存取數量
        /// </summary>
        public static int IndexCount
        {
            get => Instance.IndexCount;

            set => Instance.IndexCount = value;
        }

        /// <summary>
        /// 輸入更換重疊時動作選擇
        /// </summary>
        public static ESameEncounter SameEncounter
        {
            get => Instance.SameEncounter; 
            
            set => Instance.SameEncounter = value; 
        }

        #endregion
    }
}