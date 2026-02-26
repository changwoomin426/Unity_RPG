using System.Collections.Generic;
using UnityEngine;

namespace RPG {
    public class UIQuestWindow : MonoBehaviour {
        private static UIQuestWindow _instance;
        [SerializeField] private GameObject _window;
        [SerializeField] private RectTransform _contentTransform;
        [SerializeField] private UIQuestListItem _itemPrefab;
        [SerializeField] private float _itemHeight = 60f;
        [SerializeField] private List<UIQuestListItem> _items = new List<UIQuestListItem>();
        public static bool IsOn { get { return _instance._window.activeSelf; } }

        private void Awake() {
            if (_instance == null) {
                _instance = this;
                Initialize();

            } else {
                Destroy(gameObject);
            }
        }

        private void Initialize() {
            QuestManager.Instance.SubscribeOnQuestStarted(OnQuestStarted);
            QuestManager.Instance.SubscribeOnQuestCompleted(OnQuestCompleted);
            QuestManager.Instance.SubscribeOnQuestCompleteCountChanged(OnQuestCompleteCountChanged);

            for (int ix = 1; ix < DataManager.Instance.questData._quests.Count; ++ix) {
                QuestData.Quest quest = DataManager.Instance.questData._quests[ix];
                UIQuestListItem newItem = Instantiate(_itemPrefab, _contentTransform);
                newItem.SetClosed();
                newItem.SetQuestTitle(quest.questTitle);
                newItem.SetQuestCount(0, quest.countToComplete);
                _items.Add(newItem);
            }

            Vector2 size = _contentTransform.sizeDelta;
            size.y = (DataManager.Instance.questData._quests.Count - 1) * _itemHeight;
            _contentTransform.sizeDelta = size;
        }

        public static void Show() {
            _instance._window.SetActive(true);
        }

        public static void Close() {
            _instance._window.SetActive(false);
        }

        private void OnQuestStarted(int questID) {
            _items[questID - 1].SetProgress();
        }

        private void OnQuestCompleted(int questID) {
            _items[questID - 1].SetCompleted();
        }

        private void OnQuestCompleteCountChanged(int questID, int completeCount) {
            _items[questID - 1].SetQuestCount(completeCount);
        }
    }
}