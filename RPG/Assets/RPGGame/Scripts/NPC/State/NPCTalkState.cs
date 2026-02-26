using UnityEngine;

namespace RPG {
    public class NPCTalkState : NPCStateBase {
        private QuestManager _questManager;

        protected override void OnEnable() {
            base.OnEnable();

            if (_questManager == null) {
                _questManager = QuestManager.Instance;
            }

            switch (_questManager.CheckNPCState(_manager.NPCID)) {
                case QuestManager.EQuestState.Start:
                    _questManager.MoveToNextQuest();
                    Dialogue.ShowDialogueText(_questManager.CurrentQuest.questBeginText);
                    _questManager.SetState(QuestManager.EQuestState.Processing);
                    break;
                case QuestManager.EQuestState.Processing:
                    Dialogue.ShowDialogueText(_questManager.CurrentQuest.questProgressText);
                    break;
                default:
                    Dialogue.ShowDialogueText(_questManager.CurrentQuest.smallTalk);
                    break;
            }
        }

        protected override void Update() {
            base.Update();

            if (!CanTalk()) {
                Dialogue.CloseDialogueAfterTime(3f);
                _manager.SetState(NPCStateManager.EState.Idle);
            }

            Vector3 direction = _manager.PlayerTransform.position - _refTransform.position;
            direction.y = 0f;
            _refTransform.rotation = Quaternion.LookRotation(direction);
        }
    }
}