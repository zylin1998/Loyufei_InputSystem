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

        [SerializeField]
        private EInputType _InputType;

        private void Awake()
        {
            if (Instance) { Destroy(Instance); }
            
            Instance = this;
        }
    }
}
