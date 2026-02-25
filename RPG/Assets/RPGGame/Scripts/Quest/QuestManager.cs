using UnityEngine;
using UnityEngine.Events;

namespace RPG {
    public class QuestManager : MonoBehaviour {
        public enum EQuestState {
            None = -1,
            Start,
            Processing,
            Complete,
            Length,
        }

        public static QuestManager Instance { get; private set; } = null;
        [SerializeField] private int _currentQuestID = 0;
        [SerializeField] private EQuestState _state = EQuestState.None;
        [SerializeField] private int _completeCount = 0;
        [SerializeField] private bool _isQuestCompleted = false;
        [SerializeField] private UnityEvent<int> _onQuestStarted;
        [SerializeField] private UnityEvent<int> _onQuestCompleted;
        [SerializeField] private UnityEvent _onAllQuestsCompleted;
        [SerializeField] private UnityEvent<int, int> _onQuestCompleteCountChanged;

        public QuestData.Quest CurrentQuest {
            get {
                return DataManager.Instance.questData._quests[_currentQuestID];
            }
        }

        public QuestData.Quest NextQuext {
            get {
                int nextQuestID = _currentQuestID >= DataManager.Instance.questData._quests.Count - 1 ?
                    _currentQuestID : _currentQuestID + 1;

                return DataManager.Instance.questData._quests[nextQuestID];
            }
        }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(gameObject);
            }
        }

        private void OnEnable() {
            CheckState();
            _onQuestStarted?.AddListener(SpawnMonsters);
        }

        public void SetState(EQuestState state) {
            this._state = state;
        }

        public EQuestState CheckState() {
            if (_completeCount >= CurrentQuest.countToComplete) {
                _state = EQuestState.Complete;
            } else if (_currentQuestID > 0) {
                _state = EQuestState.Processing;
            }

            return _state;
        }

        public void ProcessQuest(QuestData.EType type, QuestData.ETargetType targetType) {
            if (_isQuestCompleted) {
                return;
            }

            if (_state == EQuestState.Complete) {
                return;
            }

            if (CurrentQuest.type != type || CurrentQuest.targetType != targetType) {
                string message =
                    $"현재 진행 중인 퀘스트가 아닙니다. {CurrentQuest.type} != {type} && {CurrentQuest.targetType} != {targetType}";

                Dialogue.ShowDialogueTextTemporarily(message);
                return;
            }

            ++_completeCount;
            _onQuestCompleteCountChanged?.Invoke(_currentQuestID, _completeCount);

            if (CheckState() == EQuestState.Complete) {
                if (_currentQuestID < DataManager.Instance.questData._quests.Count - 1) {
                    Dialogue.ShowDialogueTextTemporarily($"{CurrentQuest.questTitle} 퀘스트를 완료했습니다. \n다음 퀘스트를 진행할 수 있습니다. 담당 NPC와 대화하세요.");
                } else {
                    _isQuestCompleted = true;
                    _onAllQuestsCompleted?.Invoke();
                }

                _onQuestCompleted?.Invoke(_currentQuestID);
            }
        }

        public EQuestState CheckNPCState(int npcID) {
            if (_currentQuestID == NextQuext.startCondition &&
                _state == EQuestState.Complete &&
                NextQuext.npcID == npcID) {
                return EQuestState.Start;
            }

            if (_currentQuestID == CurrentQuest.questID &&
                _state == EQuestState.Processing &&
                CurrentQuest.npcID == npcID) {
                return EQuestState.Processing;
            }

            return EQuestState.None;
        }

        public void MoveToNextQuest() {
            ++_currentQuestID;
            _completeCount = 0;
            _onQuestStarted?.Invoke(_currentQuestID);
        }

        public void SpawnMonsters(int questID) {
            if (CurrentQuest.type == QuestData.EType.Kill &&
                CurrentQuest.targetType == QuestData.ETargetType.Chomper) {
                MonsterSpawner.SpawnMonsters(CurrentQuest.countToComplete, CurrentQuest.monsterLevel);
            } else if (CurrentQuest.type == QuestData.EType.Wave &&
                        CurrentQuest.targetType == QuestData.ETargetType.Chomper) {
                MonsterSpawner.StartMonsterWave();
            }
        }

        public void SubscribeOnQuestStarted(UnityAction<int> listener) {
            _onQuestStarted?.AddListener(listener);
        }

        public void SubscribeOnQuestCompleted(UnityAction<int> listener) {
            _onQuestCompleted?.AddListener(listener);
        }

        public void SubscribeOnQuestCompleteCountChanged(UnityAction<int, int> listener) {
            _onQuestCompleteCountChanged?.AddListener(listener);
        }
    }
}