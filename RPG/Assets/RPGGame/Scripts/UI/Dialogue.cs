using UnityEngine;
using TMPro;
using System.Collections;

namespace RPG {
    [DefaultExecutionOrder(-1)]
    public class Dialogue : MonoBehaviour {
        [SerializeField] private GameObject _dialogueWindow;
        [SerializeField] private TextMeshProUGUI _dialogueText;
        [SerializeField] private float _dialogueTempShowTime = 5f;
        [SerializeField] private float _textAnimationInterval = 0.04f;
        private static Dialogue instance = null;

        private void Awake() {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(gameObject);
            }
        }

        public static void ShowDialogue() {
            instance.StopAllCoroutines();
            instance._dialogueWindow.SetActive(true);
        }

        public static void CloseDialogue() {
            instance._dialogueWindow.SetActive(false);
        }

        public static void CloseDialogueAfterTime(float time) {
            instance.StartCoroutine(instance.CloseDialogueWithDelay(time));
        }

        public static void ShowDialogueText(string text) {
            ShowDialogue();
            instance.StartCoroutine(instance.SetTextWithAnimation(text));
        }

        public static void ShowDialogueTextTemporarily(string text, float time = 0f) {
            ShowDialogue();
            instance.StopAllCoroutines();
            float dialogueShowTime = time == 0f ? instance._dialogueTempShowTime : time;
            instance.StartCoroutine(instance.SetTempDialogueTextWithAnimation(text, dialogueShowTime));
        }

        private IEnumerator SetTempDialogueTextWithAnimation(string text, float time) {
            yield return instance.StartCoroutine(instance.SetTextWithAnimation(text));
            yield return instance.StartCoroutine(instance.CloseDialogueWithDelay(time));
        }

        private IEnumerator SetTextWithAnimation(string text) {
            int count = 1;
            WaitForSeconds interval = new WaitForSeconds(_textAnimationInterval);

            while (count <= text.Length) {
                yield return interval;
                instance._dialogueText.text = text.Substring(0, count);
                ++count;
            }
        }

        private IEnumerator CloseDialogueWithDelay(float delay) {
            yield return new WaitForSeconds(delay);
            CloseDialogue();
        }
    }
}