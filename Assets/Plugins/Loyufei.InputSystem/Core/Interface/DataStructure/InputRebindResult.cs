using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    public struct InputRebindResult
    {
        public bool       Successed  { get; }
        public int        ListIndex  { get; }
        public EInputType InputType  { get; }
        public int        TargetUUID { get; }
        public int        SameUUID   { get; }
        public EInputCode TargetCode { get; }
        public EInputCode SameCode   { get; }

        public InputRebindResult(bool success, int inputList, EInputType inputType, BindingPair target, BindingPair same) 
        {
            Successed  = success;
            ListIndex  = inputList;
            InputType  = inputType;
            TargetUUID = target.UUID;
            SameUUID   = same?.UUID ?? 0;
            TargetCode = target.InputCode;
            SameCode   = same?.InputCode ?? EInputCode.None;
        }
    }
}
