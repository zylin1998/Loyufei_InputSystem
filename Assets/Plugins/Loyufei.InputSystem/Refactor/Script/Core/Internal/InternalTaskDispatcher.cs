using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

namespace Loyufei.InputSystem
{
    public class InternalTaskDispatcher : MonoBehaviour
    {
        #region Const Field

        public const int minIndex = 1;
        public const int maxIndex = 4;

        private const int inputDivide = 600;

        #endregion

        [SerializeField, Range(minIndex, maxIndex)]
        private int             _IndexCount = 1;
        [SerializeField]
        private ESameEncounter  _SameEncounter = ESameEncounter.None;
        [SerializeField]
        private List<AxisPair>  _Axis = new();
        [SerializeField]
        private UIControlInput  _UIControl;
        [SerializeField]
        private List<InputList> _InputLists = new();

        #region Internal Properties

        internal UIControlInput UIControl => _UIControl;

        internal Dictionary<int, IInput> Inputs { get; } = new();

        internal int IndexCount
        {
            get => _IndexCount;

            set
            {
                if (_IndexCount == value) { return; }

                _IndexCount = value > maxIndex ? maxIndex : value;
                _IndexCount = value < minIndex ? minIndex : value;
            }
        }

        internal IInputList DefaultInputs
        {
           get; 
            
           set;
        } = IInputList.Default;

        internal ESameEncounter SameEncounter 
        {
            get => _SameEncounter; 
            
            set => _SameEncounter = value; 
        }

        #endregion

        #region Unity Behaviour

        private void Awake()
        {
            var inputModule = EventSystem.current?.GetComponent<BaseInputModule>();

            if (inputModule)
            {
                _UIControl = inputModule.gameObject.AddComponent<UIControlInput>();
                
                inputModule.inputOverride = _UIControl;
            }
        }

        #endregion

        #region Internal Method

        /// <summary>
        /// �P�_��J���ެO�_����
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsIndexValid(int index)
        {
            if (index < minIndex) { return false; }
            if (index > maxIndex) { return false; }

            return true;
        }

        /// <summary>
        /// �]�mUI�����J�ݭn����J����
        /// </summary>
        /// <param name="index"></param>
        internal void SetUIControl(int index) 
        {
            _UIControl.SetIndex(index, Inputs[index]);
        }

        /// <summary>
        /// �]�m��J�b��T
        /// </summary>
        /// <param name="inputAxis"></param>
        internal void SetAxis(IInputAxis inputAxis) 
        {
            _Axis = inputAxis.GetPairs().ToList();
        }

        /// <summary>
        /// ���o�ηs�W��J
        /// </summary>
        /// <param name="index"></param>
        /// <param name="inputBindings"></param>
        /// <returns></returns>
        internal IInput FetchInput(int index, IInputBindings inputBindings, EInputType inputType)
        {
            var exist = Inputs.TryGetValue(index, out var input);

            if (!exist)
            {
                var constructor = AxisConstructorFactory.Create(inputType);

                input = new InputBase(constructor);

                if (input is IInputBinder binder) { binder.Binding(_Axis, inputBindings); }

                Inputs.TryAdd(index, input);
            }

            return input;
        }

        /// <summary>
        /// ���o��J�M��
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        internal InputList FetchList(int index, EInputType inputType) 
        {
            var list = _InputLists.Find(l => l.Index == index);

            if (list) { return list; }

            list = new GameObject("InputList" + index).AddComponent<InputList>();

            list.SetIndex(index);
            list.SetInputType(inputType);
            list.Init(DefaultInputs);

            list.transform.SetParent(transform);

            _InputLists.Add(list);

            return list;
        }

        /// <summary>
        /// ���m�M�檺�ƶq
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        internal IEnumerable<IInputList> Resize(int size) 
        {
            return (_InputLists = SizeCheck(size).ToList());
        }
        
        /// <summary>
        /// �����w��J
        /// </summary>
        /// <param name="listIndex"></param>
        /// <param name="uuid"></param>
        /// <returns></returns>
        internal async Task<bool> ChangeInput(int listIndex, int uuid) 
        {
            var input = await AwaitInputCodeDown();
            var list  = FetchList(listIndex, EInputType.KeyBoard);

            return list.ChangeInput(uuid, input, SameEncounter);
        }

        #endregion

        #region Private Method

        /// <summary>
        /// ��z�M��ƶq
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        internal IEnumerable<InputList> SizeCheck(int size)
        {
            var index = 0;

            foreach (var input in _InputLists) 
            {
                if (input.Index <= size) 
                {
                    yield return input;

                    index++;
                }

                else 
                {
                    Destroy(input.gameObject); 
                }
            }
        }

        /// <summary>
        /// ���B���ݫ���Q���U
        /// </summary>
        /// <returns></returns>
        private async Task<EInputCode> AwaitInputCodeDown() 
        {
            var list = Enum
                .GetValues(typeof(EInputCode))
                .OfType<KeyCode>()
                .ToArray();

            var input = EInputCode.None;

            for(; input == EInputCode.None;) 
            {
                input = (EInputCode)list.FirstOrDefault(key => Input.GetKeyDown(key));

                await Task.Yield();
            }

            return input;
        }

        #endregion
    }
}
