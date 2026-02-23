using UnityEngine;
using UnityEngine.Events;

namespace RPG {
    public class HPController : MonoBehaviour {
        [SerializeField] private float _currentHP = 0f;
        [SerializeField] private float _maxHP = 0f;
        [SerializeField] private float _defense = 0f;
        [SerializeField] private UnityEvent<float, float> _onHPChanged;
        [SerializeField] private UnityEvent _onDead;

        public void SetMaxHP(float maxHP) {
            _maxHP = maxHP;
            _currentHP = maxHP;

            _onHPChanged?.Invoke(_currentHP, maxHP);
        }

        public void SetDefense(float defense) {
            _defense = defense;
        }

        public virtual void OnHealed(float hpAmount) {
            _currentHP += hpAmount;
            _currentHP = Mathf.Clamp(_currentHP, 0f, _maxHP);
            _onHPChanged?.Invoke(_currentHP, _maxHP);
        }

        public virtual void OnDamaged(float damage) {
            float finalDamage = Mathf.Max(0f, damage - _defense);
            _currentHP -= finalDamage;
            _currentHP = Mathf.Max(0f, _currentHP);
            _onHPChanged?.Invoke(_currentHP, _maxHP);

            if (_currentHP == 0f) {
                _onDead?.Invoke();
            }
        }

        public virtual void Die() {
            _currentHP = 0f;
            _onHPChanged?.Invoke(_currentHP, _maxHP);
            _onDead?.Invoke();
        }

        public void SubscribeOnDead(UnityAction listener) {
            _onDead.AddListener(listener);
        }
    }
}