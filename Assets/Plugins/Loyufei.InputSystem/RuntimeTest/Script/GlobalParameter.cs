using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Loyufei.InputSystem.RuntimeTest
{
    internal class GlobalParameter : MonoBehaviour
    {
        public static GlobalParameter Instance { get; private set; }

        public static EInputType InputType => Instance._InputType;
        public static int PlayerIndex => Instance._PlayerIndex;

        [SerializeField]
        private EInputType _InputType;
        [SerializeField, Range(1, 4)]
        private int _PlayerIndex;

        private void Awake()
        {
            if (Instance) { Destroy(Instance); }
            
            Instance = this;
        }

        private void OnDestroy()
        {
            if (Equals(Instance, this))
            {
                Instance = default;
            }
        }
    }
}
