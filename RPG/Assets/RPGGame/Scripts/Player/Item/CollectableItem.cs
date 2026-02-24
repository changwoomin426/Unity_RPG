using UnityEngine;
using UnityEngine.Events;

namespace RPG {
    [RequireComponent(typeof(Collider))]
    public class CollectableItem : MonoBehaviour {
        [SerializeField] protected Item _item;
        [SerializeField] private bool _shouldDeleteAfterCollected = true;
        [SerializeField] protected UnityEvent _onItemCollected;
        protected Transform _refTransform;
        public bool HasCollected { get; protected set; } = false;
        public Item Item { get { return _item; } }

        protected virtual void Awake() {
            if (_refTransform == null) {
                _refTransform = transform;
            }
        }

        private void OnTriggerEnter(Collider other) {
            OnCollect(other);
        }

        protected virtual void OnCollect(Collider other) {
            if (_item == null) {
                Debug.Log("_item 변수가 설정되지 않았습니다.");
                return;
            }

            if (HasCollected || !other.CompareTag("Player")) {
                return;
            }

            InventoryManager.Instance.OnItemCollected(this);

            if (_shouldDeleteAfterCollected) {
                Destroy(gameObject);
            }

            _onItemCollected?.Invoke();
            Dialogue.ShowDialogueTextTemporarily(Item.MessageWhenCollected);
        }
    }
}