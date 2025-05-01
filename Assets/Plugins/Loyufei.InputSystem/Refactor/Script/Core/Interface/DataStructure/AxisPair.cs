using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Loyufei.InputSystem
{
    [Serializable]
    public class AxisPair
    {
        [SerializeField]
        private string _AxisName;
        [SerializeField]
        private int    _PositiveIndex;
        [SerializeField]
        private int    _NegativeIndex;
        [SerializeField]
        private bool   _Revert;
        [SerializeField, Range(0.01f, 1f)]
        private float  _Sensitive;

        public string AxisName      => _AxisName;
        public int    PositiveIndex => _PositiveIndex;
        public int    NegativeIndex => _NegativeIndex;
        public bool   Revert        => _Revert;
        public float  Sensitive     => _Sensitive;

        public AxisPair(string axisName, int positiveIndex, int negativeIndex, bool revert, float sensitive)
        {
            _AxisName      = axisName;
            _PositiveIndex = positiveIndex;
            _NegativeIndex = negativeIndex;
            _Revert        = revert;
            _Sensitive     = sensitive;
        }
    }
}
