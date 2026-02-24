using System.Collections;
using TMPro;
using UnityEngine;

namespace RPG {
    public class UIDamageText : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _damageText;
        [SerializeField] private float _scaleMax = 1.5f;
        [SerializeField] private float _scaleMin = 0.8f;
        [SerializeField] private float _scaleAnimationTime = 0.5f;
        private float _originalFontSize;
        private float _elapsedTime = 0f;

        private void Awake() {
            _originalFontSize = _damageText.fontSize;
        }

        public void OnDamageRecived(float damage) {
            _damageText.text = $"-{damage}";
            StartCoroutine(PlayScaleAnimation());
        }

        private IEnumerator PlayScaleAnimation() {
            _damageText.fontSize = _originalFontSize * _scaleMax;
            _elapsedTime = 0f;

            while (_elapsedTime <= _scaleAnimationTime) {
                yield return null;
                _elapsedTime += Time.deltaTime;
                float scale = Mathf.Lerp(_scaleMax, _scaleMin, _elapsedTime / _scaleAnimationTime);
                _damageText.fontSize = _originalFontSize * scale;
            }

            SetEmpty();
        }

        private void SetEmpty() {
            _damageText.text = string.Empty;
            _damageText.fontSize = _originalFontSize;
        }
    }
}