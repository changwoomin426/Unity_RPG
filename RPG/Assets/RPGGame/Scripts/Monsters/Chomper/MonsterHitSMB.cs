using UnityEngine;

namespace RPG {
    public class MonsterHitSMB : StateMachineBehaviour {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            animator.ResetTrigger("Hit");
        }
    }
}