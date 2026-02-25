using UnityEngine;

namespace RPG {
    public class CameraRig : MonoBehaviour {
        [SerializeField] private Transform _followTarget;
        [SerializeField] private float _movementDelay = 5f;
        private Transform _refTransform;
        private Camera _refCamera;
        [SerializeField] private float _rotationDelay = 5f;
        [SerializeField] private float _rotationSpeed = 0.2f;
        [SerializeField] private Vector2 _rotationXMinMax = new Vector2(-20f, 25f);
        private float _xRotation = 0f;
        private float _yRotation = 0f;

        private void Awake() {
            if (_refTransform == null) {
                _refTransform = transform;
            }

            if (_followTarget == null) {
                _followTarget = GameObject.FindGameObjectWithTag("Player").transform;
            }

            if (_refCamera == null) {
                _refCamera = Camera.main;
            }
        }

        private void LateUpdate() {
            _refTransform.position = Vector3.Lerp(_refTransform.position, _followTarget.position, _movementDelay * Time.deltaTime);

            if (UIInventoryWindow.IsOn) {
                return;
            }

            _xRotation -= InputManager.MouseMove.y * _rotationSpeed;
            _xRotation = Mathf.Clamp(_xRotation, _rotationXMinMax.x, _rotationXMinMax.y);
            _yRotation += InputManager.MouseMove.x * _rotationSpeed;
            Quaternion startRotation = _refTransform.rotation;
            Quaternion endRotation = Quaternion.Euler(new Vector3(_xRotation, _yRotation, 0f));
            _refTransform.rotation = Quaternion.Slerp(startRotation, endRotation, _rotationDelay * Time.deltaTime);
        }
    }
}