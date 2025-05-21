using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    public interface IInputAxis
    {
        /// <summary>
        /// 輸入軸清單的層級
        /// </summary>
        public int Layer { get; }
        /// <summary>
        /// 取得所有輸入軸資訊
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AxisPair> GetPairs();

        /// <summary>
        /// 預設輸入軸資訊
        /// </summary>
        public static IInputAxis Default { get; } = new DefaultPairs();

        private class DefaultPairs : IInputAxis 
        {
            public int Layer => int.MinValue;

            private List<AxisPair> _Pairs = new();

            public IEnumerable<AxisPair> GetPairs() => _Pairs;
        }
    }
}
