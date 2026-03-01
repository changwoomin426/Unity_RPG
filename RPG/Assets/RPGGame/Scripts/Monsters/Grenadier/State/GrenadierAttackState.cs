using UnityEngine;
using UnityEngine.Events;

namespace RPG {
    public class GrenadierAttackState : GrenadierStateBase {
        [SerializeField] private UnityEvent _onMeleeAttackStart;
        [SerializeField] private UnityEvent _onMeleeAttackCheckStart;
        [SerializeField] private UnityEvent _onRangeAttackStart;
        [SerializeField] private UnityEvent _OnAttackEnd;
        [SerializeField] private GrenadierAnimationController _animationController;

        protected override void OnEnable() {
            base.OnEnable();

            if (_manager.IsPlayerDead) {
                _manager.SetState(GrenadierStateManager.EState.Idle);
            }

            if (_animationController == null) {
                _animationController = GetComponentInChildren<GrenadierAnimationController>();
            }

            if (_manager.CurrentAttackType == GrenadierStateManager.EAttackType.Melee) {
                _onMeleeAttackCheckStart?.Invoke();
            }
        }

        protected override void Update() {
            base.Update();
            _manager.RotateToPlayer();

            if (!Util.IsInSight(_refTransform, _manager.PlayerTransform, _manager.Data.sightAngle, _manager.Data.rangeAttackRange)) {
                _manager.SetState(GrenadierStateManager.EState.Idle);
                return;
            }
        }

        public void StartAttack() {
            if (_manager.CurrentAttackType == GrenadierStateManager.EAttackType.Melee) {
                _onMeleeAttackCheckStart?.Invoke();
            }
        }

        private void ActivateShield() {
            _onRangeAttackStart?.Invoke();
        }

        private void EndAttack() {
            _manager.SetState(GrenadierStateManager.EState.Idle);
            _OnAttackEnd?.Invoke();
            _animationController.ResetHit();
        }

        public void SubscribeOnMeleeAttackStart(UnityAction listener) {
            _onMeleeAttackStart?.Invoke();
        }

        public void SubScribeOnMeleeCheckStart(UnityAction listener) {
            _onMeleeAttackCheckStart?.Invoke();
        }

        public void SubscribeOnRangeAttackStart(UnityAction listener) {
            _onRangeAttackStart?.AddListener(listener);
        }

        public void SubscribeOnAttackEnd(UnityAction listener) {
            _OnAttackEnd?.AddListener(listener);
        }
    }
}