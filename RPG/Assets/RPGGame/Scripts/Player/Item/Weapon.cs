using UnityEngine;

namespace RPG {
    public class Weapon : CollectableItem {
        [SerializeField] private float _attackAmount = 0f;
        [SerializeField] private float _radius = 0.1f;
        [SerializeField] private ParticleSystem _hitParticle;
        [SerializeField] private Transform[] _attackPoints;
        [SerializeField] private LayerMask _attackTargetLayer;
        private bool _isInAttack = false;

        protected override void Awake() {
            base.Awake();

            WeaponItem weaponItem = _item as WeaponItem;
            if (weaponItem != null) {
                _attackAmount = weaponItem.Attack;
            }
        }

        private void FixedUpdate() {
            if (!_isInAttack) {
                return;
            }

            Collider[] colliders = Physics.OverlapCapsule(
                _attackPoints[0].position,
                _attackPoints[1].position,
                _radius,
                 _attackTargetLayer
                 );

            if (colliders.Length == 0) {
                return;
            }

            foreach (Collider collider in colliders) {
                // Util.LogRed("무기와 충돌함");
                Damageable damageable = collider.GetComponent<Damageable>();
                if (damageable != null) {
                    damageable.ReceiveDamage(_attackAmount);
                }

                if (_hitParticle != null) {
                    _hitParticle.transform.position = collider.transform.position;
                    _hitParticle.Play();
                }
            }
        }

        protected override void OnCollect(Collider other) {
            // base.OnCollect(other);

            if (!HasCollected && other.CompareTag("Player")) {
                WeaponController weaponController = other.GetComponentInChildren<WeaponController>();
                if (weaponController != null) {
                    weaponController.AttachWeapon(this);
                    // MonsterSpawner.SpawnMonsters(5, 2);
                }

                _onItemCollected?.Invoke();
            }

        }

        public void Attach(Transform parentTransform) {
            _refTransform.SetParent(parentTransform);
            _refTransform.localPosition = Vector3.zero;
            _refTransform.localRotation = Quaternion.identity;
            HasCollected = true;
        }

        public void OnAttackBegin() {
            _isInAttack = true;
        }

        public void OnAttackEnd() {
            _isInAttack = false;
        }
    }
}