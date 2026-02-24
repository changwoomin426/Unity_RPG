using UnityEngine;

namespace RPG {
    public class AutoDestroy : MonoBehaviour {
        [SerializeField] private float _destroyTime = 2f;

        public void Destroy() {
            GameObject.Destroy(gameObject, _destroyTime);
        }
    }
}