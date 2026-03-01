using UnityEngine;

namespace RPG {
    public class GrenadierRangeAttack : MonoBehaviour {
        [SerializeField] private float _attackAmount = 0f;
        [SerializeField] private float _radius = 1f;
        [SerializeField] private LayerMask _attackLayerMask;
        private Transform _refTransform;

        private void Awake() {
            if (_refTransform == null) {
                _refTransform = transform;
            }

            GrenadierAttackState attackState = GetComponentInParent<GrenadierAttackState>();
            if (attackState != null) {
                attackState.SubscribeOnRangeAttackStart(Attack);
            }
        }

        public void Attack() {
            Collider[] colliders = Physics.OverlapSphere(
                _refTransform.position,
                _radius,
                _attackLayerMask
                );

            if (colliders.Length == 0) {
                return;
            }

            foreach (Collider collider in colliders) {
                Damageable damageable = collider.GetComponent<Damageable>();
                if (damageable == null) {
                    continue;
                }
                damageable.ReceiveDamage(_attackAmount);
            }
        }

        public void SetAttack(float attack) {
            _attackAmount = attack;
        }

        public void SetAttackRange(float range) {
            _radius = range;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos() {
            if (_refTransform == null) {
                _refTransform = transform;
            }

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(_refTransform.position, _radius);
        }
#endif
    }
}