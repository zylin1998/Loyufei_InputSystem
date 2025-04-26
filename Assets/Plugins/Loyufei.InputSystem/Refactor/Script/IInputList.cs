using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace InputSystem
{
    public interface IInputList
    {
        public void Init(IInputList inputList);

        /// <summary>
        /// f\取得所有輸入資訊
        /// </summary>
        /// <returns></returns>
        public IEnumerable<InputPair> GetPairs();

        /// <summary>
        /// 基礎輸入類別，為空輸入
        /// </summary>
        public static IInputList Default { get; } = new DefaultList();

        private class DefaultList : IInputList 
        {
            private List<InputPair> _Pairs = new();

            public void Init(IInputList inputList) 
            {
                //No Use
            }

            public IEnumerable<InputPair> GetPairs() => _Pairs;
        }
    }
}
