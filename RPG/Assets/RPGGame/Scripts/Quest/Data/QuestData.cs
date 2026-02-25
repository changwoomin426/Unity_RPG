using System.Collections.Generic;
using UnityEngine;

namespace RPG {
    [CreateAssetMenu]
    public class QuestData : ScriptableObject {
        public enum EType {
            None = -1,
            CollectWeapon,
            Kill,
            Wave,
            Length,
        }

        public enum ETargetType {
            None = -1,
            Player,
            Chomper,
            Grenadier,
            Length,
        }

        [System.Serializable]
        public class Quest {
            public int questID = 0;
            public string questTitle = string.Empty;
            public EType type = EType.None;
            public ETargetType targetType = ETargetType.None;
            public int countToComplete = 0;
            public float exp = 0f;
            public string questBeginText = string.Empty;
            public string questProgressText = string.Empty;
            public string smallTalk = string.Empty;
            public int startCondition = 0;
            public int nextQuestID = 0;
            public int npcID = 0;
            public int monsterLevel = 0;
        }

        public List<Quest> _quests = new List<Quest>();
    }
}