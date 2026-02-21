using UnityEngine;

namespace RPG {
    [CreateAssetMenu(fileName = "New Weapon Item", menuName = "Inventory/Item/WeaponItem")]
    public class WeaponItem : Item {
        public float Attack;

        public void Awake() {
            ItemName = "무기";
        }
    }
}