using System;
using UnityEngine;
using UnityEngine.Events;

namespace RPG {
    public class MonsterStateManager : MonoBehaviour {
        public enum EState {
            None = -1,
            Idle,
            Patrol,
            Chase,
            Attack,
            Length,
        }

        [SerializeField] private EState _state = EState.None;
        [SerializeField] private MonsterStateBase[] _states;
        [SerializeField] private UnityEvent<EState> _onStateChanged;
        [SerializeField] private MonsterData _data;
        private Transform _refTransform;
        [SerializeField] private int _level = 1;
        public MonsterData.LevelData CurrentLevelData { get; private set; }
        public Transform PlayerTransform { get; private set; }

        private void Awake() {
            _data = DataManager.Instance.monsterData;
            CurrentLevelData = _data.levels[_level - 1];
            _states = new MonsterStateBase[(int)EState.Length];

            for (int ix = 0; ix < (int)EState.Length; ++ix) {
                string componentName = $"RPG.Monster{(EState)ix}State";
                _states[ix] = GetComponent(Type.GetType(componentName)) as MonsterStateBase;
                _states[ix].enabled = false;

                if (_data != null) {
                    _states[ix].SetData(_data);
                }
            }

            if (_refTransform == null) {
                _refTransform = transform;
            }

            if (PlayerTransform == null) {
                PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform.root;
            }
        }

        private void OnEnable() {
            SetState(EState.Idle);
        }

        private void Update() {
            if (_state == EState.Idle || _state == EState.Patrol) {
                if (Util.IsInSight(_refTransform, PlayerTransform, _data.sightAngle, _data.attackRange)) {
                    SetState(EState.Chase);
                    return;
                }
            }

            if (_state == EState.Chase || _state == EState.Attack) {
                if (!Util.IsInSight(_refTransform, PlayerTransform, _data.sightAngle, _data.sightRange)) {
                    SetState(EState.Idle);
                    return;
                }
            }
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

        public void SubscribeOnStateChange(UnityAction<EState> listener) {
            _onStateChanged?.AddListener(listener);
        }

        public void SetLevel(int level) {
            _level = level;
            CurrentLevelData = _data.levels[level - 1];
        }
    }
}