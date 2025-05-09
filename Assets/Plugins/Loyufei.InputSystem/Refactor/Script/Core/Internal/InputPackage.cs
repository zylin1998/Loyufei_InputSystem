using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Loyufei.InputSystem
{
    [Serializable]
    public struct InputPackage
    {
        [SerializeField]
        private List[] _Lists;

        public InputPackage(IEnumerable<IInputList> lists) 
        {
            _Lists = lists.Select(list => new List(list)).ToArray();
        }

        public IEnumerable<IInputList> GetLists() => _Lists.OfType<IInputList>();

        [Serializable]
        public struct List : IInputList
        {
            [SerializeField]
            private EInputType  _InputType;
            [SerializeField]
            private InputPair[] _Pairs;

            public List(IInputList list) : this()
            {
                Init(list);
            }

            public EInputType InputType => _InputType;

            IEnumerable<InputPair> IInputList.GetPairs()
            {
                return _Pairs;
            }

            public void Init(IInputList inputList)
            {
                _InputType = inputList.InputType;

                _Pairs     = inputList.GetPairs().ToArray(); ;
            }
        }
    }
}
