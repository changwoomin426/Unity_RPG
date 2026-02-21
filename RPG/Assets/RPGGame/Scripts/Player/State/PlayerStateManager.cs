using UnityEngine;
using UnityEngine.Events;

namespace RPG {
    public class PlayerStateManager : MonoBehaviour {
        public enum EState {
            None = -1,
            Idle,
            Move,
            Jump,
            Attack,
            Length,
        }

        public enum EAttackCombo {
            None = -1,
            Combo1,
            Combo2,
            Combo3,
            Combo4,
            Length,
        }

        [SerializeField] private EState _state = EState.None;
        [SerializeField] private PlayerStateBase[] _states;
        [SerializeField] private UnityEvent<EState> _onStateChanged;
        [SerializeField] private PlayerData _data;
        private CharacterController _characterController;
        public bool IsGrounded { get; private set; }
        private PlayerAnimationController _animationController;
        public EAttackCombo NextAttackCombo { get; private set; } = EAttackCombo.None;
        private WeaponController _weaponController;

        private void Awake() {
            // _data = Resources.Load("Data/Player Data") as PlayerData;
            _data = DataManager.Instance.playerData;

            if (_characterController == null) {
                _characterController = GetComponent<CharacterController>();
            }

            if (_animationController == null) {
                _animationController = GetComponentInChildren<PlayerAnimationController>();
            }

            PlayerAttackState attackState = GetComponent<PlayerAttackState>();
            if (attackState != null) {
                attackState.SubscribeOnAttackEnd(OnAttackEnd);
            }

            if (_weaponController == null) {
                _weaponController = GetComponentInChildren<WeaponController>();
            }

            for (int ix = 0; ix < _states.Length; ++ix) {
                if (_data != null) {
                    _states[ix].SetData(_data);
                }
            }
        }

        private void OnEnable() {
            SetState(EState.Idle);
        }

        private void Update() {
            if (_state == EState.Jump) {
                return;
            }

            if (InputManager.IsAttack) {
                if (_state != EState.Attack && _weaponController.IsWeaponAttached) {
                    NextAttackCombo = EAttackCombo.Combo1;
                    SetState(EState.Attack);
                    _animationController.SetAttackComboState((int)NextAttackCombo);
                    return;
                }

                AnimatorStateInfo currentAnimationState = _animationController.GetCurrentStateInfo();

                if (currentAnimationState.IsName("AttackCombo1")) {
                    NextAttackCombo = EAttackCombo.Combo2;
                } else if (currentAnimationState.IsName("AttackCombo2")) {
                    NextAttackCombo = EAttackCombo.Combo3;
                } else if (currentAnimationState.IsName("AttackCombo3")) {
                    NextAttackCombo = EAttackCombo.Combo4;
                } else {
                    NextAttackCombo = EAttackCombo.None;
                }

                return;
            }

            if (_state == EState.Attack) {
                return;
            }

            if (IsGrounded && _state == EState.Move && InputManager.IsJump) {
                IsGrounded = false;
                SetState(EState.Jump);
                return;
            }

            if (InputManager.Movement == Vector2.zero) {
                SetState(EState.Idle);
            } else {
                SetState(EState.Move);
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

        private void OnAnimatorMove() {
            IsGrounded = _characterController.isGrounded;
        }

        private void OnAttackEnd() {
            NextAttackCombo = EAttackCombo.None;
            SetState(EState.Idle);
            _animationController.SetAttackComboState((int)NextAttackCombo);
        }
    }
}