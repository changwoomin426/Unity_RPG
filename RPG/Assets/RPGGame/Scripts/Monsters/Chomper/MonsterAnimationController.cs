using UnityEngine;

namespace RPG {
    public class MonsterAnimationController : MonoBehaviour {
        private Animator _refAnimator;

        private void Awake() {
            if (_refAnimator == null) {
                _refAnimator = GetComponentInParent<Animator>();
            }

            MonsterStateManager manager = GetComponentInParent<MonsterStateManager>();

            if (manager != null) {
                manager.SubscribeOnStateChange(OnStateChanged);
            }
        }

        public void OnStateChanged(MonsterStateManager.EState state) {
            if (_refAnimator == null) {
                return;
            }

            _refAnimator.SetInteger("State", (int)state);
        }

        public void OnDamaged() {
            _refAnimator.SetTrigger("Hit");
        }
    }
}