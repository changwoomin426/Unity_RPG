using UnityEngine;

namespace RPG {
    public class GrenadierMeleeAttack : MonoBehaviour {
        [SerializeField] private float _attackAmount = 0f;
        [SerializeField] private float _radius = 0.2f;
        [SerializeField] private Transform[] _attackPoints;
        [SerializeField] private LayerMask _attackTargetLayer;
        [SerializeField] private Transform _parent;
        private bool _isInAttack = false;

        private void Awake() {
            GrenadierAttackState attackState = GetComponentInParent<GrenadierAttackState>();
            if (attackState != null) {
                attackState.SubScribeOnMeleeCheckStart(OnAttackBegin);
                attackState.SubscribeOnAttackEnd(OnAttackEnd);
            }

            Transform refTransform = transform;
            refTransform.SetParent(_parent);
            refTransform.localPosition = Vector3.zero;
            refTransform.localRotation = Quaternion.identity;
        }

        public void SetAttack(float attack) {
            _attackAmount = attack;
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
                Damageable damageable = collider.GetComponent<Damageable>();
                if (damageable != null) {
                    damageable.ReceiveDamage(_attackAmount);
                }
            }
        }

        public void OnAttackBegin() {
            _isInAttack = true;
        }

        public void OnAttackEnd() {
            _isInAttack = false;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            foreach (Transform point in _attackPoints) {
                Gizmos.DrawWireSphere(point.position, _radius);
            }
        }
#endif
    }
}