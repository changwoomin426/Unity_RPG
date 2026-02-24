using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RPG {
    public class HPBar : MonoBehaviour {
        [SerializeField] private Image _hpBar;
        [SerializeField] private TextMeshProUGUI _hpGaugeText;

        public void OnDamageReceived(float currentHP, float maxHP) {
            if (_hpBar != null) {
                _hpBar.fillAmount = currentHP / maxHP;
            }

            if (_hpGaugeText != null) {
                _hpGaugeText.text = $"{currentHP}/{maxHP}";
            }
        }
    }
}