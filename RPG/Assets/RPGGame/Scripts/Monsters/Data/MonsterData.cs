using System.Collections.Generic;
using UnityEngine;

namespace RPG {
    public class MonsterData : ScriptableObject {
        [System.Serializable]
        public class LevelData {
            public int level = 1;
            public float maxHP = 0f;
            public float attack = 0f;
            public float rangeAttack = 0f;
            public float defense = 0f;
            public float gainExp = 0f;
        }

        public List<LevelData> levels;
        public float patrolWaitTime = 3f;
        public float patrolRotateSpeed = 360f;
        public float sightAngle = 60f;
        public float sightRange = 10f;
        public float chaseRotateSpeed = 540f;
        public float attackRange = 2f;
        public float rangeAttackRange = 6f;

    }
}