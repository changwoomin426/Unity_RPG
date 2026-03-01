using UnityEngine;

namespace RPG {
    public class GrenadierHitEffectPlayer : MonoBehaviour {
        [SerializeField] private ParticleSystem _hitParticle;
        [SerializeField] private SkinnedMeshRenderer _mainRenderer;
        [SerializeField] private float _hitEffectTime = 1f;
        [SerializeField] private Color _hitEffectColor = Color.red;
        private Material _colorMaterial;
        private Color _originalColor;

        private void Awake() {
            if (_colorMaterial == null) {
                _colorMaterial = _mainRenderer.materials[1];
                _originalColor = _colorMaterial.GetColor("_Color2");
            }
        }

        public void Hit() {
            _hitParticle.Play();
            _colorMaterial.SetColor("_Color2", _hitEffectColor);
            Invoke(nameof(ReturnOriginalColor), _hitEffectTime);
        }

        private void ReturnOriginalColor() {
            _colorMaterial.SetColor("_Color2", _originalColor);
        }
    }
}