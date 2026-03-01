using UnityEngine;

namespace RPG {
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour {
        [SerializeField] private bool _isRandomizePitch = false;
        [SerializeField] private float _pitchRandomRange = 0.2f;
        [SerializeField] private float _playDelay = 0f;
        [SerializeField] private AudioClip[] _audioClips;

        private AudioSource _audioPlayer;

        private void OnEnable() {
            if (_audioPlayer == null) {
                _audioPlayer = GetComponent<AudioSource>();
            }
        }

        public void Play() {
            AudioClip clip = _audioClips[Random.Range(0, _audioClips.Length)];
            _audioPlayer.pitch = _isRandomizePitch ? Random.Range(1.0f - _pitchRandomRange, 1.0f + _pitchRandomRange) : 1.0f;
            _audioPlayer.clip = clip;
            _audioPlayer.PlayDelayed(_playDelay);
        }
    }
}