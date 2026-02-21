using UnityEngine;

namespace RPG {
    public class PlayerMoveState : PlayerStateBase {
        // [SerializeField] private float _rotationSpeed = 540f;

        protected override void Update() {
            base.Update();
            Vector3 direction = new Vector3(InputManager.Movement.x, 0f, InputManager.Movement.y);

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