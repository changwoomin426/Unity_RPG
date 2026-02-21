using UnityEngine;
using UnityEngine.Events;

namespace RPG {
    public class PlayerJumpState : PlayerStateBase {
        // [SerializeField] private float _jumpPower = 8f;
        [SerializeField] private float _verticalSpeed = 0f;
        // [SerializeField] private float _gravityInJump = 10f;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private UnityEvent _onJumpEnd;

        protected override void OnEnable() {
            base.OnEnable();
            // _verticalSpeed = _jumpPower;
            _verticalSpeed = _data.jumpPower;
        }

        protected override void Update() {
            Vector3 movement = Vector3.zero;

            if (_verticalSpeed < 0f && _manager.IsGrounded) {
                _onJumpEnd?.Invoke();
                _manager.SetState(PlayerStateManager.EState.Idle);
            } else {
                if (_verticalSpeed > 0f) {
                    _verticalSpeed -= _data.gravityInJump * Time.deltaTime;
                }

                if (Mathf.Approximately(_verticalSpeed, 0f)) {
                    _verticalSpeed = 0f;
                }

                _verticalSpeed += Physics.gravity.y * Time.deltaTime;
                movement = _moveSpeed * _refTransform.forward * Time.deltaTime;
                movement += Vector3.up * _verticalSpeed * Time.deltaTime;
            }

            _characterController.Move(movement);
        }
    }
}