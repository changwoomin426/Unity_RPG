using UnityEngine;

namespace RPG {
    public class CameraRig : MonoBehaviour {
        [SerializeField] private Transform _followTarget;
        [SerializeField] private float _movementDelay = 5f;
        private Transform _refTransform;

        private void Awake() {
            if (_refTransform == null) {
                _refTransform = transform;
            }

            if (_followTarget == null) {
                _followTarget = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

        private void LateUpdate() {
            _refTransform.position = Vector3.Lerp(_refTransform.position, _followTarget.position, _movementDelay * Time.deltaTime);
        }
    }
}