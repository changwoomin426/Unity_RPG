using UnityEngine;

namespace RPG {
    public abstract class Item : ScriptableObject {
        public string ItemName;
        public Sprite Sprite;
        [TextArea(2, 15)] public string MessageWhenCollected;
        [TextArea(2, 15)] public string MessageWhenUsed;

        public virtual void Use() {
            InventoryManager.Instance.OnItemUsed(this);
            Dialogue.ShowDialogueTextTemporarily(MessageWhenUsed);
        }
    }
}