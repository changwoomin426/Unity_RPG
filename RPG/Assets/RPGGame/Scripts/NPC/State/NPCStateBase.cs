using UnityEngine;

namespace RPG {
    public class NPCStateBase : MonoBehaviour {
        protected Transform _refTransform;
        protected CharacterController _characterController;
        protected NPCStateManager _manager;

        protected virtual void OnEnable() {
            if (_refTransform == null) {
                _refTransform = transform;
            }

            if (_characterController == null) {
                _characterController = GetComponent<CharacterController>();
            }

            if (_manager == null) {
                _manager = GetComponent<NPCStateManager>();
            }
        }

        protected virtual void Update() {
            _characterController.Move(Physics.gravity * Time.deltaTime);
        }

        protected bool CanTalk() {
            return Vector3.Distance(
                _manager.PlayerTransform.position, _refTransform.position) <= _manager.Data.interactionSight;
        }
    }
}