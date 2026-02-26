using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RPG {
    public class UIQuestListItem : MonoBehaviour {
        [SerializeField] private Image _questStatusImage;
        [SerializeField] private Color _closeColor;
        [SerializeField] private Color _progressColor;
        [SerializeField] private Color _completeColor;
        [SerializeField] private TextMeshProUGUI _questStatusText;
        private readonly string CLOSE_TEXT = "닫힘";
        private readonly string PROGRESS_TEXT = "진행";
        private readonly string COMPLETE_TEXT = "완료";
        [SerializeField] private TextMeshProUGUI _questTitleText;
        [SerializeField] private TextMeshProUGUI _questCountText;
        private int _completeCount = 0;

        public void SetClosed() {
            _questStatusImage.color = _progressColor;
            _questStatusText.text = CLOSE_TEXT;
        }

        public void SetProgress() {
            _questStatusImage.color = _progressColor;
            _questStatusText.text = PROGRESS_TEXT;
        }

        public void SetCompleted() {
            _questStatusImage.color = _completeColor;
            _questStatusText.text = COMPLETE_TEXT;

            SetQuestCount(_completeCount);
        }

        public void SetQuestTitle(string questTitle) {
            _questTitleText.text = questTitle;
        }

        public void SetQuestCount(int currentCompleteCount) {
            _questCountText.text = $"{currentCompleteCount}/{_completeCount}";
        }

        public void SetQuestCount(int currentCompleteCount, int countsToCompleteQuest) {
            _completeCount = countsToCompleteQuest;
            _questCountText.text = $"{currentCompleteCount}/{countsToCompleteQuest}";
        }
    }
}