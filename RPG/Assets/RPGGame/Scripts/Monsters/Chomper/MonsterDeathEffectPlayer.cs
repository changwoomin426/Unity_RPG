using UnityEngine;

namespace RPG {
    public class MonsterDeathEffectPlayer : MonoBehaviour {
        private readonly string CUTOFF_PARAMETER_NAME = "_Cutoff";
        [SerializeField] private float _startTime = 1.2f;
        [SerializeField] private float _playTime = 2.0f;
        private Renderer[] _renderers;
        private float _elapsedTime = 0f;
        private bool _isPlaying = false;
        private MaterialPropertyBlock _propertyBlock;

        private void OnEnable() {
            _renderers = transform.root.GetComponentsInChildren<Renderer>();
            _propertyBlock = new MaterialPropertyBlock();
        }

        private void Update() {
            if (!_isPlaying) {
                return;
            }

            foreach (Renderer renderer in _renderers) {
                renderer.GetPropertyBlock(_propertyBlock);
                _propertyBlock.SetFloat(CUTOFF_PARAMETER_NAME, _elapsedTime / _playTime);
                renderer.SetPropertyBlock(_propertyBlock);
            }

            _elapsedTime += Time.deltaTime;

            if (_elapsedTime > _playTime) {
                _isPlaying = false;
                Destroy(transform.root.gameObject);
            }
        }

        public void Play() {
            Invoke(nameof(PlayDeathEffect), _startTime);
        }

        private void PlayDeathEffect() {
            _elapsedTime = 0f;
            _isPlaying = true;
        }
    }
}