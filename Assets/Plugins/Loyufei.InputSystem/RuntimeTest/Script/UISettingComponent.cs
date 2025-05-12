using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if LOYUFEI_INPUTSYSTEM

namespace Loyufei.InputSystem.RuntimeTest
{
    public class UISettingComponent : MonoBehaviour
    {
        [SerializeField]
        private GameObject         _Main;
        [SerializeField]
        private Transform          _Mask;
        [SerializeField]
        private List<InputChanger> _Changers;

        public IInput Input { get; private set; }

        public InputChanger this[int uuid] => _Changers.Find(c => c.UUID == uuid);

        private void Awake()
        {
            gameObject.SetActive(false);

            InputManager.SameEncounter = ESameEncounter.Exchange;
        }

        private void OnEnable()
        {
            Time.timeScale = 0f;

            _Changers[0].Selected();
        }

        private void OnDisable()
        {
            Time.timeScale = 1f;
        }

        private void Start()
        {
            Input = InputManager.FetchInput(1, EInputType.KeyBoard);

            _Changers.ForEach(c => c.AddListener(() => Rebind(c.UUID)));

            foreach (var pair in Input.FetchList().GetPairs()) 
            {
                this[pair.UUID]?.SetContext(pair.InputCode);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown("Cancel"))
            {
                gameObject.SetActive(false);

                _Main.SetActive(true);
            }
        }

        private async void Rebind(int uuid) 
        {
            _Mask.gameObject.SetActive(true);

            var result = await Input.Rebind(uuid);
            
            if (result.Successed) 
            {
                this[result.TargetUUID]?.SetContext(result.TargetCode);
                this[result.SameUUID]  ?.SetContext(result.SameCode);
            }

            _Mask.gameObject.SetActive(false);
        }

        public void ResetInput() 
        {
            foreach (var pair in Input.Reset())
            {
                this[pair.UUID]?.SetContext(pair.InputCode);
            }
        }
    }
}

#endif
