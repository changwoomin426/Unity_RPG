using UnityEngine;
using UnityEngine.Events;

namespace RPG {
    public class Damageable : MonoBehaviour {
        [SerializeField] private UnityEvent<float> _onDamageReceived;
        [SerializeField] private bool _isInvulnerable = false;
        [SerializeField] private float _invulnerableTime = 0.2f;
        [SerializeField] private float _time = 0f;
        [SerializeField] private float _defense = 0f;

        private void Update() {
            if (_isInvulnerable && Time.time > _time + +_invulnerableTime) {
                _isInvulnerable = false;
            }
        }

        public void SetDefense(float defense) {
            _defense = defense;
        }

        public void ReceiveDamage(float damageAmount) {
            if (_isInvulnerable) {
                return;
            }

            _isInvulnerable = true;
            _time = Time.time;
            float finalDamage = damageAmount - _defense;
            finalDamage = Mathf.Max(0f, finalDamage);
            _onDamageReceived?.Invoke(finalDamage);
            Util.Log($"ReceiveDamage:{transform.root.name}, damage:{damageAmount}");
        }
    }
}