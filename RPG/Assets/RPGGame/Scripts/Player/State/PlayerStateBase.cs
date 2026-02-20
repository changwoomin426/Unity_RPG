using UnityEngine;

namespace RPG {
    public class PlayerStateBase : MonoBehaviour {
        protected Transform _refTransform;
        protected CharacterController _characterController;
        protected Animator _refAnimator;
        protected PlayerStateManager _manager;
        protected PlayerAnimationController _animationController;

        protected virtual void OnEnable() {
            if (_refTransform == null) {
                _refTransform = transform;
            }

            if (_characterController == null) {
                _characterController = GetComponent<CharacterController>();
            }

            if (_refAnimator == null) {
                _refAnimator = GetComponent<Animator>();
            }

            if (_manager == null) {
                _manager = GetComponent<PlayerStateManager>();
            }

            if (_animationController == null) {
                _animationController = GetComponentInChildren<PlayerAnimationController>();
            }
        }

        protected virtual void Update() {
            _characterController.Move(Physics.gravity * Time.deltaTime);
        }

        protected virtual void OnDisable() { }

        protected virtual void OnAnimatorMove() {
            _characterController.Move(_refAnimator.deltaPosition);
        }
    }
}

