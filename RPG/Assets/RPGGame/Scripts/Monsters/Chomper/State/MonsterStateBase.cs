using UnityEngine;

namespace RPG {
    public class MonsterStateBase : MonoBehaviour {
        protected Transform _refTransform;
        protected Animator _refAnimator;
        protected CharacterController _characterController;
        protected MonsterData _data;
        protected MonsterStateManager _manager;

        protected virtual void OnEnable() {
            if (_refTransform == null) {
                _refTransform = transform;
            }

            if (_refAnimator == null) {
                _refAnimator = GetComponent<Animator>();
            }

            if (_characterController == null) {
                _characterController = GetComponent<CharacterController>();
            }

            if (_manager == null) {
                _manager = GetComponent<MonsterStateManager>();
            }
        }

        protected virtual void Update() {
            if (_characterController.enabled) {
                _characterController.Move(Physics.gravity * Time.deltaTime);
            }
        }

        protected virtual void OnDisable() { }

        public void SetData(MonsterData data) {
            _data = data;
        }

        protected virtual void OnAnimatorMove() {
            if (_characterController.enabled) {
                _characterController.Move(_refAnimator.deltaPosition);
            }
        }
    }
}