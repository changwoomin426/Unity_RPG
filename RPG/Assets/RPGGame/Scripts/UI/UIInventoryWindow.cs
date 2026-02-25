using System.Collections.Generic;
using UnityEngine;

namespace RPG {
    public class UIInventoryWindow : MonoBehaviour {
        private static UIInventoryWindow instance;
        [SerializeField] private GameObject _window;
        [SerializeField] private RectTransform _contentTransform;
        [SerializeField] private UIInventoryListItem _itemPrefab;
        [SerializeField] private float _itemWidth;
        [SerializeField] private float _itemHeight;
        [SerializeField] private List<UIInventoryListItem> _items = new List<UIInventoryListItem>();
        public static bool IsOn { get { return instance._window.activeSelf; } }

        private void OnEnable() {
            InventoryManager.Instance.SubscribeOnItemListChanged(OnItemListChanged);
        }

        private void Awake() {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(gameObject);
            }
        }

        public static void OnItemListChanged() {
            if (instance._items.Count == InventoryManager.Instance.ItemCount) {
                foreach (var item in instance._items) {
                    ItemSlot itemSlot = InventoryManager.Instance.GetItemSlot(item.item);
                    item.SetCount(itemSlot.count);
                }
                return;
            }

            foreach (var item in instance._items) {
                Destroy(item.gameObject);
            }

            instance._items.Clear();
            const int maxXCount = 4;
            List<ItemSlot> itemList = InventoryManager.Instance.GetItems();

            foreach (ItemSlot itemSlot in itemList) {
                UIInventoryListItem newItem = Instantiate(instance._itemPrefab, instance._contentTransform);
                newItem.SetItem(itemSlot.item);
                newItem.SetSprite(itemSlot.item.Sprite);
                newItem.SetName(itemSlot.item.ItemName);
                newItem.SetCount(itemSlot.count);
                instance._items.Add(newItem);
            }

            Vector2 contentSize = instance._contentTransform.sizeDelta;
            float lineCount = itemList.Count / maxXCount + 1;
            contentSize.y = lineCount * (instance._itemHeight) + (lineCount - 1) * 10f * 20f;
            instance._contentTransform.sizeDelta = contentSize;
        }

        public static void Show() {
            instance._window.SetActive(true);
        }

        public static void Close() {
            instance._window.SetActive(false);
        }
    }
}