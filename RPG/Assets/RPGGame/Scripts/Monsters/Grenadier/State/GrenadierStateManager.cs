using System;
using UnityEngine;
using UnityEngine.Events;

namespace RPG {
    [DefaultExecutionOrder(-1)]
    public class GrenadierStateManager : MonoBehaviour {
        public enum EState {
            None = -1,
            Idle,
            Rotate,
            Attack,
            Dead,
            Length,
        }

        public enum EAttackType {
            None = -1,
            Melee,
            Range,
            Length,
        }

        [SerializeField] private EState _state = EState.None;
        [SerializeField] private GrenadierStateBase[] _states;
        [SerializeField] private UnityEvent<EState> _onStateChanged;
        [SerializeField] private UnityEvent<EAttackType> _onAttackTypeChanged;
        [SerializeField] private int _level = 1;
        private PlayerStateManager TargetPlayerStateManager { get; set; }
        private Transform _refTransform;
        [SerializeField] private float _attackInterval = 3f;
        [SerializeField] private float _attackTime = 0f;

        private bool CanAttack {
            get {
                return Time.time > _attackTime + _attackInterval;
            }
        }

        public EAttackType CurrentAttackType { get; private set; } = EAttackType.None;

        public bool IsPlayerDead {
            get {
                return TargetPlayerStateManager.IsPlayerDead;
            }
        }

        public MonsterData Data;
        public MonsterData.LevelData CurrentLevelData { get; private set; }
        public Transform PlayerTransform { get; private set; }

        private void Awake() {
            if (_refTransform == null) {
                _refTransform = transform;
            }

            Data = DataManager.Instance.grenadierData;
            CurrentLevelData = Data.levels[_level - 1];
            _states = new GrenadierStateBase[(int)EState.Length];

            for (int ix = 0; ix < _states.Length; ++ix) {
                string componentName = $"RPG.Grenadier{(EState)ix}State";
                _states[ix] = GetComponent(Type.GetType(componentName)) as GrenadierStateBase;
                _states[ix].enabled = false;
            }

            TargetPlayerStateManager = FindAnyObjectByType<PlayerStateManager>();
            PlayerTransform = TargetPlayerStateManager.transform;
            HPController hpController = GetComponentInChildren<HPController>();
            if (hpController != null) {
                hpController.SetMaxHP(CurrentLevelData.maxHP);
                hpController.SetDefense(CurrentLevelData.defense);
                hpController.SubscribeOnDead(OnDead);
            }

            GrenadierMeleeAttack meleeAttack = GetComponentInChildren<GrenadierMeleeAttack>();
            if (meleeAttack != null) {
                meleeAttack.SetAttack(CurrentLevelData.attack);
            }

            GrenadierRangeAttack rangeAttack = GetComponentInChildren<GrenadierRangeAttack>();
            if (rangeAttack != null) {
                rangeAttack.SetAttack(CurrentLevelData.rangeAttack);
                rangeAttack.SetAttackRange(Data.rangeAttackRange);
            }
        }

        private void OnEnable() {
            SetState(EState.Idle);
        }

        public void SetState(int state) {
            SetState((EState)state);
        }

        public void SetState(EState newState) {
            if (_state == EState.Dead || _state == newState) {
                return;
            }

            if (_state != EState.None) {
                _states[(int)_state].enabled = false;
            }

            _states[(int)newState].enabled = true;
            _state = newState;
            _onStateChanged?.Invoke(_state);
        }

        public void ChangeToAttack() {
            if (_state == EState.Attack || IsPlayerDead) {
                return;
            }

            if (!CanAttack) {
                return;
            }

            float distanceToPlayer = (PlayerTransform.position - _refTransform.position).sqrMagnitude;

            if (distanceToPlayer > Data.rangeAttackRange * Data.attackRange) {
                CurrentAttackType = EAttackType.None;
                return;
            }

            if ((distanceToPlayer > Data.attackRange * Data.attackRange) &&
                distanceToPlayer <= Data.rangeAttackRange * Data.rangeAttackRange) {
                // Debug.Log("Range");
                CurrentAttackType = EAttackType.Range;
            } else if (distanceToPlayer <= Data.attackRange * Data.attackRange) {
                // Debug.Log("Melee");
                CurrentAttackType = EAttackType.Melee;
            }

            _attackTime = Time.time;
            _onAttackTypeChanged?.Invoke(CurrentAttackType);
            SetState(EState.Attack);
        }

        public void OnDead() {
            SetState(EState.Dead);
        }

        public void RotateToPlayer() {
            Vector3 direction = PlayerTransform.position - _refTransform.position;
            direction.y = 0f;
            _refTransform.rotation = Quaternion.LookRotation(direction);
        }

        public void SubscribeOnStateChanged(UnityAction<EState> listener) {
            _onStateChanged?.AddListener(listener);
        }

        public void SubscribeOnAttackTypeChanged(UnityAction<EAttackType> listener) {
            _onAttackTypeChanged?.AddListener(listener);
        }
    }
}