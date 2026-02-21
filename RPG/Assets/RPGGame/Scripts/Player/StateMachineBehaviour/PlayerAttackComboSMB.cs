using UnityEngine;

namespace RPG {
    public class PlayerAttackComboSMB : StateMachineBehaviour {
        [SerializeField] private int _effectIndex = -1;
        private WeaponController _weaponController;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (_effectIndex == -1) {
                return;
            }

            if (_weaponController == null) {
                _weaponController = animator.GetComponentInChildren<WeaponController>();
            }

            _weaponController.PlayAttackComboEffect(_effectIndex);
        }
    }
}