using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG {
    [DefaultExecutionOrder(-1)]
    public class InputManager : MonoBehaviour {
        public static Vector2 Movement { get; private set; } = Vector2.zero;
        public static bool IsJump { get; private set; } = false;
        public static bool IsAttack { get; private set; } = false;

        private InputAction _moveAction;
        private InputAction _jumpAction;
        private InputAction _attackAction;

        private void Awake() {
            if (_moveAction == null) {
                _moveAction = InputSystem.actions.FindAction("Move");
            }

            if (_jumpAction == null) {
                _jumpAction = InputSystem.actions.FindAction("Jump");
            }

            if (_attackAction == null) {
                _attackAction = InputSystem.actions.FindAction("Attack");
            }
        }

        private void Update() {
            Movement = _moveAction.ReadValue<Vector2>();
            IsJump = _jumpAction.WasPressedThisFrame();
            IsAttack = _attackAction.WasPressedThisFrame();
        }
    }
}