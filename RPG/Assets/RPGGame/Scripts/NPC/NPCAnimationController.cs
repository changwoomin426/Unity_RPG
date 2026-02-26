using UnityEngine;

namespace RPG {
    public class NPCAnimationController : MonoBehaviour {
        [SerializeField] private Animator _refAnimator;

        private void OnEnable() {
            _refAnimator = transform.parent.GetComponentInChildren<Animator>();
            var stateManager = transform.parent.GetComponentInChildren<NPCStateManager>();

            if (stateManager) {
                stateManager.SubscribeOnStateChanged(OnStateChanged);
            }
        }

        private void OnStateChanged(NPCStateManager.EState state) {
            if (_refAnimator == null) {
                return;
            }

            _refAnimator.SetInteger("State", (int)state);
        }
    }
}