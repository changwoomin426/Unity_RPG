using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

namespace RPG {
    public class PlayerLevelController : MonoBehaviour {
        [SerializeField] private int _level = 0;
        [SerializeField] private float _currentExp = 0f;
        [SerializeField] private UnityEvent<int> _onLevelUp;
        [SerializeField] private TextMeshProUGUI _levelGaugeText;
        [SerializeField] private Image _expBar;
        [SerializeField] private TextMeshProUGUI _expGaugeText;

        private int _maxLevel;
        private bool IsMaxLevel { get { return _level == _maxLevel; } }

        private void Awake() {
            _maxLevel = DataManager.Instance.playerData.levels.Count;
            UpdateExpBar();
            UpdateLevelText();
        }

        private void UpdateExpBar() {
            if (IsMaxLevel) {
                if (_expBar != null) {
                    _expBar.fillAmount = 1f;
                }

                if (_expGaugeText != null) {
                    _expGaugeText.text = "MAX";
                }

                return;
            }

            int nextLeveIndex = _level;
            float requiredExpForNextLevel = DataManager.Instance.playerData.levels[nextLeveIndex].requiredExp;
            float requiredExpForCurrentLevel = DataManager.Instance.playerData.levels[_level - 1].requiredExp;

            if (_expBar != null) {
                float expAmount = (_currentExp - requiredExpForCurrentLevel) / (requiredExpForNextLevel - requiredExpForCurrentLevel);
                _expBar.fillAmount = expAmount;
            }

            if (_expGaugeText != null) {
                _expGaugeText.text = $"{_currentExp - requiredExpForCurrentLevel}/{requiredExpForNextLevel - requiredExpForCurrentLevel}";
            }
        }

        private void UpdateLevelText() {
            if (_levelGaugeText != null) {
                if (IsMaxLevel) {
                    _levelGaugeText.text = $"{_maxLevel}/{_maxLevel}";
                    return;
                }

                _levelGaugeText.text = $"{_level}/{_maxLevel}";
            }
        }

        public void GainExp(float exp) {
            _currentExp += exp;
            UpdateExpBar();

            if (IsMaxLevel) {
                return;
            }

            int oldLevelIndex = _level - 1;
            int targetLevelIndex = 0;

            for (int ix = targetLevelIndex; ix < _maxLevel; ++ix) {
                if (_currentExp < DataManager.Instance.playerData.levels[ix].requiredExp) {
                    targetLevelIndex = ix - 1;
                    break;
                }
            }

            if (_level > 1 &&
                _currentExp > 0f &&
                targetLevelIndex == 0) {
                targetLevelIndex = _maxLevel - 1;
            }

            if (oldLevelIndex != targetLevelIndex) {
                _level = targetLevelIndex + 1;
                _onLevelUp?.Invoke(_level);
                UpdateExpBar();
                UpdateLevelText();
            }
        }

        public void SubscribeOnLevelUp(UnityAction<int> listener) {
            _onLevelUp.AddListener(listener);
        }
    }
}