using System;
using UnityEngine;
using UnityEngine.Events;

namespace RPG {
    public class NPCStateManager : MonoBehaviour {
        public enum EState {
            None = -1,
            Idle,
            Talk,
            Length,
        }

        [SerializeField] private EState _state = EState.None;
        [SerializeField] private NPCStateBase[] _states;
        [SerializeField] private UnityEvent<EState> _onStateChanged;
        [SerializeField] private int _npcID = 0;
        public int NPCID { get { return _npcID; } }
        public NPCData.Attribute Data { get; private set; } = null;
        public Transform PlayerTransform { get; private set; } = null;

        private void Awake() {
            PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            Data = DataManager.Instance.npcData._attributes[_npcID - 1];
            _states = new NPCStateBase[(int)EState.Length];

            for (int ix = 0; ix < (int)_states.Length; ++ix) {
                string componentName = $"RPG.NPC{(EState)ix}State";
                _states[ix] = GetComponent(Type.GetType(componentName)) as NPCStateBase;
                _states[ix].enabled = false;
            }
        }

        private void OnEnable() {
            SetState(EState.Idle);
        }

        public void SetState(EState newState) {
            if (_state == newState) {
                return;
            }

            if (_state != EState.None) {
                _states[(int)_state].enabled = false;
            }

            _states[(int)newState].enabled = true;
            _state = newState;
            _onStateChanged?.Invoke(_state);
        }

        public void SubscribeOnStateChanged(UnityAction<EState> listener) {
            _onStateChanged?.AddListener(listener);
        }
    }
}