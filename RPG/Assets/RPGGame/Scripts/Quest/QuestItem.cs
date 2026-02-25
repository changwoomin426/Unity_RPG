using UnityEngine;

namespace RPG {
    public class QuestItem : MonoBehaviour {
        [SerializeField] private QuestData.EType _type = QuestData.EType.None;
        [SerializeField] private QuestData.ETargetType _targetType = QuestData.ETargetType.None;

        public void SetType(QuestData.EType type) {
            _type = type;
        }

        public virtual void OnCompleted() {
            if (_type == QuestData.EType.None ||
                _targetType == QuestData.ETargetType.None) {
                return;
            }

            QuestManager.Instance.ProcessQuest(_type, _targetType);
        }
    }
}