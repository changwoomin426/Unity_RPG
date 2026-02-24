using UnityEngine;

namespace RPG {
    public class UIBillborad : MonoBehaviour {
        private Transform _refTransform;
        private Camera _mainCamera;

        private void Awake() {
            if (_refTransform == null) {
                _refTransform = transform;
            }

            if (_mainCamera == null) {
                _mainCamera = Camera.main;
            }
        }

        private void LateUpdate() {
            _refTransform.forward = _mainCamera.transform.forward;
        }
    }
}