using UnityEngine;

namespace RPG {
    public class MonsterPatrolState : MonsterStateBase {
        [SerializeField] private float _patrolDistance = 6f;
        [SerializeField] private Vector3 _patrolDestination;
        [SerializeField] float _validPartolTime = 3f;
        [SerializeField] private Transform _pointer;
        [SerializeField] private bool _test = false;
        private float _patrolStartTime = 0f;

        protected override void OnEnable() {
            base.OnEnable();

            if (_test) {
                _pointer.SetParent(null);
                _pointer.position = Vector3.zero;
            }

            if (Util.RandomPoint(_refTransform.position, _patrolDistance, out _patrolDestination)) {
                _patrolStartTime = Time.time;

                if (_test) {
                    _pointer.position = _patrolDestination;
                    _pointer.gameObject.SetActive(true);
                } else {
                    _pointer.gameObject.SetActive(false);
                }
            } else {
                ResetPointer();
                _manager.SetState(MonsterStateManager.EState.Idle);
            }
        }

        protected override void Update() {
            base.Update();

            if (Time.time > _patrolStartTime + _validPartolTime) {
                _manager.SetState(MonsterStateManager.EState.Idle);
            }

            if (Util.IsArrived(_refTransform, _patrolDestination, 0.5f)) {
                ResetPointer();
                _manager.SetState(MonsterStateManager.EState.Idle);
            }

            Vector3 direction = _patrolDestination - _refTransform.position;
            direction.y = 0f;

            if (direction != Vector3.zero) {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                _refTransform.rotation = Quaternion.RotateTowards(
                    _refTransform.rotation,
                    targetRotation,
                    _data.patrolRotateSpeed * Time.deltaTime
                );
            }
        }

        protected override void OnDisable() {
            base.OnDisable();
            _patrolDestination = Vector3.zero;

            if (_test) {
                ResetPointer();
            }
        }

        private void ResetPointer() {
            if (!transform.root.gameObject.activeInHierarchy) {
                return;
            }

            if (_test) {
                _pointer.gameObject.SetActive(false);
                _pointer.SetParent(_refTransform);
                _pointer.localPosition = Vector3.zero;
            }
        }
    }
}