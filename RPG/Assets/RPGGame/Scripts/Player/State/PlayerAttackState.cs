using UnityEngine;
using UnityEngine.Events;

namespace RPG {
    public class PlayerAttackState : PlayerStateBase {
        [SerializeField] private UnityEvent _onAttackBegin;
        [SerializeField] private UnityEvent _onAttackCheckEnd;
        [SerializeField] private UnityEvent _onAttackEnd;

        private void AttackStart() {
            _onAttackBegin?.Invoke();
        }

        public void SubscribeOnAttackBegin(UnityAction listener) {
            _onAttackBegin?.AddListener(listener);
        }

        private void AttackCheckEnd() {
            _onAttackCheckEnd?.Invoke();
        }

        public void SubscribeOnAttackCheckEnd(UnityAction listener) {
            _onAttackCheckEnd?.AddListener(listener);
        }

        private void ComboCheck() {
            _animationController.SetAttackComboState((int)_manager.NextAttackCombo);
        }

        private void AttackEnd() {
            _onAttackEnd?.Invoke();
            _manager.SetState(PlayerStateManager.EState.Idle);
        }

        public void SubscribeOnAttackEnd(UnityAction listener) {
            _onAttackEnd?.AddListener(listener);
        }
    }
}