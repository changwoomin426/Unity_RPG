using UnityEngine;
using UnityEngine.Events;

namespace RPG {
    public class PlayerStateManager : MonoBehaviour {
        public enum EState {
            None = -1,
            Idle,
            Move,
            Jump,
            Length,
        }

        [SerializeField] private EState _state = EState.None;
        [SerializeField] private PlayerStateBase[] _states;
        [SerializeField] private UnityEvent<EState> _onStateChanged;
        private CharacterController _characterController;
        public bool IsGrounded { get; private set; }

        private void Awake() {
            if (_characterController == null) {
                _characterController = GetComponent<CharacterController>();
            }
        }

        private void OnEnable() {
            SetState(EState.Idle);
        }

        private void Update() {
            if (_state == EState.Jump) {
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
    }
}