using UnityEngine;
using UnityEngine.Events;

namespace RPG {
    public class GrenadierRotateState : GrenadierStateBase {
        [SerializeField] private GrenadierAnimationController _animationController;
        [SerializeField] private UnityEvent _onPlayStep;
        [SerializeField] private float _angle = 0f;

        protected override void OnEnable() {
            base.OnEnable();

            if (_animationController == null) {
                _animationController = GetComponentInChildren<GrenadierAnimationController>();
            }
        }

        protected override void Update() {
            base.Update();
            RotateToPlayer();

            if (Util.IsInSight(_refTransform, _manager.PlayerTransform, _manager.Data.sightAngle, _manager.Data.rangeAttackRange)) {
                _manager.ChangeToAttack();
            }

            if (Vector3.Distance(_refTransform.position, _manager.PlayerTransform.position) > _manager.Data.sightRange) {
                _manager.SetState(GrenadierStateManager.EState.Idle);
            }
        }

        private void PlayStep() {
            _onPlayStep?.Invoke();
        }

        private void RotateToPlayer() {
            Vector3 direction = _manager.PlayerTransform.position - _refTransform.position;
            direction.y = 0f;

            if (direction == Vector3.zero) {
                return;
            }

            _angle = Vector3.SignedAngle(_refTransform.forward, direction, Vector3.up);

            if (Mathf.Abs(_angle) < 20f) {
                _refTransform.rotation = Quaternion.LookRotation(direction);
                return;
            }

            _angle /= 180f;
            _animationController.SetAngle(_angle);
        }


    }
}