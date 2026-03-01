using UnityEngine;

namespace RPG {
    public class GrenadierStateBase : MonoBehaviour {
        protected GrenadierStateManager _manager;
        protected Transform _refTransform;

        protected virtual void OnEnable() {
            if (_manager == null) {
                _manager = GetComponent<GrenadierStateManager>();
            }

            if (_refTransform == null) {
                _refTransform = transform;
            }
        }

        protected virtual void Update() { }
        protected virtual void OnDisable() { }
    }
}