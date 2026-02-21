using System.Collections.Generic;
using UnityEngine;

namespace RPG {
    public class PlayerData : ScriptableObject {
        [System.Serializable]
        public class LevelData {
            public int level = 1;
            public float maxHP = 0f;
            public float requiredExp = 0f;
        }

        public List<LevelData> levels;
        public float rotationSpeed = 1000f;
        public float jumpPower = 8f;
        public float gravityInJump = 10f;
    }
}