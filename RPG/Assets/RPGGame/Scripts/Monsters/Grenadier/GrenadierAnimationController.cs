using UnityEngine;

namespace RPG {
    public class GrenadierAnimationController : MonoBehaviour {
        [SerializeField] private Animator _refAnimator;

        private void Awake() {
            if (_refAnimator == null) {
                _refAnimator = transform.root.GetComponentInChildren<Animator>();
            }

            var stateManager = transform.root.GetComponentInChildren<GrenadierStateManager>();
            if (stateManager != null) {
                stateManager.SubscribeOnStateChanged(OnStateChanged);
                stateManager.SubscribeOnAttackTypeChanged(OnAttackTypeChanged);
            }
        }

        public void SetAngle(float angle) {
            _refAnimator.SetFloat("Angle", angle);
        }

        public void Hit() {
            _refAnimator.SetTrigger("Hit");
        }

        public void ResetHit() {
            _refAnimator.ResetTrigger("Hit");
        }

        private void OnStateChanged(GrenadierStateManager.EState state) {
            if (_refAnimator == null) {
                return;
            }

            _refAnimator.SetInteger("State", (int)state);
        }

        private void OnAttackTypeChanged(GrenadierStateManager.EAttackType attackType) {
            _refAnimator.SetInteger("AttackType", (int)attackType);
        }
    }
}