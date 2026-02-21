using System.Collections;
using UnityEngine;

namespace RPG {
    public class PlayerAttackEffect : MonoBehaviour {
        private Animation _refAnimation;

        private void Awake() {
            if (_refAnimation == null) {
                _refAnimation = GetComponent<Animation>();
            }

            gameObject.SetActive(false);
        }

        public void Activate() {
            gameObject.SetActive(true);
            _refAnimation.Play();
            StartCoroutine(DisableAtEndOfAnimation());
        }

        private IEnumerator DisableAtEndOfAnimation() {
            yield return new WaitForSeconds(_refAnimation.clip.length);
            gameObject.SetActive(false);
        }
    }
}