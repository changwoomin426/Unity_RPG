using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG {
    [System.Serializable]
    public class ItemSlot {
        public Item item = null;
        public int count = 0;

        public ItemSlot(Item item, int count) {
            this.item = item;
            this.count = count;
        }

        public void AddCount(int count) {
            count += count;
        }

        public void UseItem() {
            --count;
        }
    }

    [DefaultExecutionOrder(-50)]
    public class InventoryManager : MonoBehaviour {
        private static InventoryManager _instance = null;
        public static InventoryManager Instance { get { return _instance; } }
        [SerializeField] private Dictionary<Item, ItemSlot> _items = new Dictionary<Item, ItemSlot>();
        [SerializeField] private UnityEvent _onItemListChanged;
        public int ItemCount { get { return _items.Count; } }

        private void Awake() {
            if (_instance == null) {
                _instance = this;
            } else {
                Destroy(gameObject);
            }
        }

        public ItemSlot GetItemSlot(Item item) {
            if (_items.TryGetValue(item, out ItemSlot itemSlot)) {
                return itemSlot;
            }

            return null;
        }

        public List<ItemSlot> GetItems() {
            List<ItemSlot> itemSlots = new List<ItemSlot>();

            foreach (var item in _items) {
                itemSlots.Add(item.Value);
            }

            return itemSlots;
        }

        public void AddItem(Item item) {
            if (_items.ContainsKey(item)) {
                if (_items.TryGetValue(item, out ItemSlot itemSlot)) {
                    itemSlot.AddCount(1);
                    return;
                }
            } else {
                _items.Add(item, new ItemSlot(item, 1));
            }
        }

        public bool HasItem(Item item) {
            return _items.ContainsKey(item);
        }

        public void OnItemCollected(CollectableItem item) {
            AddItem(item.Item);
            _onItemListChanged?.Invoke();
        }

        public void OnItemUsed(Item item) {
            if (_items.ContainsKey(item)) {
                if (_items.TryGetValue(item, out ItemSlot itemSlot)) {
                    --itemSlot.count;

                    if (itemSlot.count == 0) {
                        _items.Remove(item);
                    }
                }
            }

            _onItemListChanged?.Invoke();
        }

        public void SubscribeOnItemListChanged(UnityAction listener) {
            _onItemListChanged?.AddListener(listener);
        }
    }
}