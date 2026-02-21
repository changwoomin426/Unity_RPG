using UnityEngine;

namespace RPG {
    public class WeaponController : MonoBehaviour {
        [SerializeField] private Transform _weaponHolder;
        [SerializeField] private Weapon _weapon;
        [SerializeField] private PlayerAttackEffect[] _attackComboEffects;
        [SerializeField] private AudioPlayer _swingSound;

        public bool IsWeaponAttached { get; private set; }

        public void AttachWeapon(Weapon weapon) {
            if (IsWeaponAttached) {
                return;
            }

            // 튜토리얼 순서는
            // _weapon.Attach(_weaponHolder);
            // _weapon = weapon;
            // 하지만 null 발생

            _weapon = weapon;
            _swingSound = GetComponentInChildren<AudioPlayer>();
            _weapon.Attach(_weaponHolder);
            IsWeaponAttached = true;

            PlayerAttackState playerAttackState = transform.root.GetComponentInChildren<PlayerAttackState>();
            if (playerAttackState != null) {
                playerAttackState.SubscribeOnAttackBegin(weapon.OnAttackBegin);
                playerAttackState.SubscribeOnAttackCheckEnd(weapon.OnAttackEnd);
            }
        }

        public void PlayAttackComboEffect(int comboIndex) {
            if (comboIndex < 0 || comboIndex >= _attackComboEffects.Length) {
                return;
            }

            _attackComboEffects[comboIndex].Activate();
            _swingSound.Play();
        }
    }
}