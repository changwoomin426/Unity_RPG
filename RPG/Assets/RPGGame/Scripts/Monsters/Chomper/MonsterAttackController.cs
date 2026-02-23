using UnityEngine;

namespace RPG {
    public class MonsterAttackController : MonoBehaviour {
        [SerializeField] private float _attackAmount = 0f;
        [SerializeField] private bool _isInAttack = false;
        [SerializeField] private float _radius = 0.5f;
        [SerializeField] private LayerMask _attackTargetLayer;
        private Transform _refTransform;

        private void Awake() {
            if (_refTransform == null) {
                _refTransform = transform;
            }
        }

        public void SetAttack(float attackAmount) {
            _attackAmount = attackAmount;
        }

        private void FixedUpdate() {
            if (!_isInAttack) {
                return;
            }

            Collider[] colliders = Physics.OverlapSphere(_refTransform.position, _radius, _attackTargetLayer);

            if (colliders.Length == 0) {
                return;
            }

            foreach (var collider in colliders) {
                Damageable damageable = collider.GetComponent<Damageable>();

                if (damageable != null) {
                    damageable.ReceiveDamage(_attackAmount);
                    _isInAttack = false;
                    return;
                }
            }
        }

        public void OnAttackBegin() {
            _isInAttack = true;
        }

        public void OnAttackEnd() {
            _isInAttack = false;
        }

        private void OnDrawGizmos() {
#if UNITY_EDITOR
            if (_refTransform == null) {
                _refTransform = transform;
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_refTransform.position, _radius);
#endif
        }
    }
}