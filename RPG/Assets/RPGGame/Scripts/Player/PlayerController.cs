using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG {
    public class PlayerController : MonoBehaviour {

        [SerializeField] private float _moveSpeed = 8f;
        [SerializeField] private float _rotationSpeed = 720f;

        [SerializeField] private Animator _refAnimator;

        private Transform _refTreansform;
        private InputAction _moveAction;


        private void Awake() {
            if (_refTreansform == null) {
                _refTreansform = transform;
            }

            if (_moveAction == null) {
                _moveAction = InputSystem.actions.FindAction("Move");
            }

            if (_refAnimator == null) {
                _refAnimator = GetComponentInChildren<Animator>();
            }
        }

        private void Update() {
            Vector2 moveValue = _moveAction.ReadValue<Vector2>();
            Vector3 direction = new Vector3(moveValue.x, 0, moveValue.y);
            direction.Normalize();
            _refTreansform.position = _refTreansform.position + direction * _moveSpeed * Time.deltaTime;

            if (direction != Vector3.zero) {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                _refTreansform.rotation = Quaternion.RotateTowards(_refTreansform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            }

            if (moveValue == Vector2.zero) {
                _refAnimator.SetInteger("State", 0);
            } else {
                _refAnimator.SetInteger("State", 1);
            }
        }

        private void PlayStep() {

        }
    }
}