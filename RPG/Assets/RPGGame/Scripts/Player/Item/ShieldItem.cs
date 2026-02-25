using UnityEngine;

namespace RPG {
    [CreateAssetMenu(fileName = "New Shield Item", menuName = "Inventory/Item/ShieldItem")]
    public class ShieldItem : Item {
        public float defense;

        private void Awake() {
            ItemName = "방어구";
        }

        public override void Use() {
            base.Use();
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null) {
                Damageable damageable = player.transform.root.GetComponentInChildren<Damageable>();

                if (damageable != null) {
                    damageable.SetDefense(defense);
                }
            }
        }
    }
}