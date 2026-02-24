using UnityEngine;

namespace RPG {
    [CreateAssetMenu(fileName = " New Health Item", menuName = "Inventory/Item/HealthItem")]
    public class HealthItem : Item {
        public float HealthAmount;

        public void Awake() {
            ItemName = "체력";
        }

        public override void Use() {
            base.Use();
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null) {
                HPController hpController = player.transform.root.GetComponentInChildren<HPController>();
                if (hpController != null) {
                    hpController.OnHealed(HealthAmount);
                }
            }
        }
    }
}