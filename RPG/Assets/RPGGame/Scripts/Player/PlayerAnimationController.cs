using UnityEngine;

namespace RPG {
    public class PlayerAnimationController : MonoBehaviour {
        [SerializeField] private Animator _refAnimator;

        private void OnEnable() {
            if (_refAnimator == null) {
                _refAnimator = GetComponentInParent<Animator>();
            }
        }

        public void OnStateChanged(PlayerStateManager.EState newState) {
            if (_refAnimator == null) {
                return;
            }

            _refAnimator.SetInteger("State", (int)newState);
        }

        public void OnLanding() {
            _refAnimator.SetTrigger("Landing");
        }

        public void SetAttackComboState(int attackCombo) {
            _refAnimator.SetInteger("AttackCombo", attackCombo);
        }

        public AnimatorStateInfo GetCurrentStateInfo() {
            return _refAnimator.GetCurrentAnimatorStateInfo(0);
        }
    }
}