using UnityEngine;

namespace RPG {
    public class PlayerMoveState : PlayerStateBase {
        // [SerializeField] private float _rotationSpeed = 540f;
        private Transform _cameraTransform;

        protected override void OnEnable() {
            base.OnEnable();

            if (_cameraTransform == null) {
                _cameraTransform = Camera.main.transform;
            }
        }

        protected override void Update() {
            base.Update();
            // Vector3 direction = new Vector3(InputManager.Movement.x, 0f, InputManager.Movement.y);
            Vector3 cameraForward = _cameraTransform.forward;
            cameraForward.y = 0f;
            cameraForward.Normalize();
            Vector3 direction = InputManager.Movement.x * _cameraTransform.right + InputManager.Movement.y * cameraForward;
            direction.y = 0f;

            if (direction.sqrMagnitude > 1.0f) {
                direction.Normalize();
            }

            if (direction != Vector3.zero) {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                _refTransform.rotation = Quaternion.RotateTowards(_refTransform.rotation, targetRotation, _data.rotationSpeed * Time.deltaTime);
            }
        }

        private void PlayStep() { }
    }
}