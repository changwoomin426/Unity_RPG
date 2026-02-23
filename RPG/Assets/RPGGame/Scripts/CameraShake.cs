using System.Collections;
using UnityEngine;


namespace RPG {
    public class CameraShake : MonoBehaviour {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private float _shakeTime = 0.4f;
        [SerializeField] private float _shakeAmount = 0.05f;
        private Vector3 _originPosition;
        private bool _isShaking = false;

        private void Awake() {
            if (_cameraTransform == null) {
                _cameraTransform = Camera.main.transform;
            }
        }

        private void OnEnable() {
            _originPosition = _cameraTransform.localPosition;
        }

        public void Play() {
            if (_isShaking) {
                return;
            }

            _originPosition = _cameraTransform.localPosition;
            StartCoroutine(nameof(ShakeCamera));
        }

        private IEnumerator ShakeCamera() {
            _isShaking = true;
            float elapsedTime = 0f;

            while (elapsedTime < _shakeTime) {
                Vector3 shakePosition = Random.insideUnitSphere * _shakeAmount;
                _cameraTransform.localPosition = _originPosition + shakePosition;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _cameraTransform.localPosition = _originPosition;
            _isShaking = false;
        }
    }
}