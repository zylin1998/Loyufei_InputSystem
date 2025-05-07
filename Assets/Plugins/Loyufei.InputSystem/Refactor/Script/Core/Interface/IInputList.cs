using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    public interface IInputList
    {
        /// <summary>
        /// 輸入清單索引
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// 以外部清單初始化清單
        /// </summary>
        /// <param name="inputList"></param>
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
