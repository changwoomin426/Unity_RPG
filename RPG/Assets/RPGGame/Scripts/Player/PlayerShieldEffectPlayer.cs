using System.Collections;
using UnityEngine;

namespace RPG {
    public class PlayerShieldEffectPlayer : MonoBehaviour {
        [SerializeField] private float _playTime = 0.5f;

        public void Play() {
            gameObject.SetActive(true);
            StartCoroutine(WaitAndTurnOff(_playTime));
        }

        private IEnumerator WaitAndTurnOff(float delay) {
            yield return new WaitForSeconds(delay);
            gameObject.SetActive(false);
        }
    }
}